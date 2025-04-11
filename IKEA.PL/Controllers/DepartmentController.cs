using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Services.DepartmentServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        #region Services | Dependency Injection
        private readonly IDepartmentServices DepartmentServices;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment environment;

        public DepartmentController(IDepartmentServices _DepartmentServices, ILogger<DepartmentController> _logger, IWebHostEnvironment environment)
        {
            DepartmentServices = _DepartmentServices;
            logger = _logger;
            this.environment = environment;
        }

        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var Departments = DepartmentServices.GetAllDepartments();
            return View(Departments);
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = DepartmentServices.GetDepartmentById(id.Value);
            
            if (department is null)
                return NotFound();

            return View(department); 
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatedDepartmentDto DepartmentDto)
        {
            //ServerSide Validation 
            if(!ModelState.IsValid)
                return View(DepartmentDto);

            var Message = string.Empty;
            try
            {
                var result = DepartmentServices.CreateDepartment(DepartmentDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    Message = "Department is not created .";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(DepartmentDto);
                }
            }
            catch (Exception ex)
            {
                //Log Exception Kestral
                logger.LogError(ex, ex.Message);
                
                //Set Default Message For User 
                if(environment.IsDevelopment())
                {
                    Message = ex.Message;
                    ModelState.AddModelError(string.Empty, Message);
                    return View(DepartmentDto);
                }
                else
                {
                    Message = "An Error Occurred While Creation";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(DepartmentDto);
                }
            }


        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id is null)
                return BadRequest();
            var department = DepartmentServices.GetDepartmentById(Id.Value);

            if (department == null)
                return NotFound();

            var MappedDepartment = new UpdatedDepartmenDto()
            {
                Id = department.id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreationDate = department.CreationDate
            };

            return View(MappedDepartment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdatedDepartmenDto departmenDto)
        {
            if (!ModelState.IsValid)
                return View(departmenDto);

            var Message = string.Empty;
            try
            {
                var result = DepartmentServices.UpdateDepartment(departmenDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    Message = "Department Is Not Updated ..!";

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                Message = environment.IsDevelopment() ? ex.Message : "An Error Has Been Occurred During Updating Department";
                throw;
            }

            ModelState.AddModelError(string.Empty, Message);

            return View(departmenDto);
        }


        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete (int? Id )
        {
            if (Id == null) return BadRequest();
            var department = DepartmentServices.GetDepartmentById(Id.Value);
            if (department is null) return NotFound();

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete (int DeptId)
        {
            var Message = string.Empty;

            try
            {
                var IsDeleted = DepartmentServices.DeleteDepartment(DeptId);
                if (IsDeleted) return RedirectToAction(nameof(Index));

                Message = " Department Is Not Deleted .";

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                Message = environment.IsDevelopment() ? ex.Message : "An Error Has Been Occurred During Deleteing Department";
                throw;
            }

            ModelState.AddModelError(string.Empty, Message);
            return RedirectToAction(nameof(Delete), new { id = DeptId });

        }
        #endregion

    }
}
