using AutoMapper;
using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.DAL.Models.Departments;
using IKEA.PL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        #region Services | Dependency Injection
        private readonly IDepartmentServices DepartmentServices;
        private readonly IMapper mapper;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment environment;

        public DepartmentController(IDepartmentServices _DepartmentServices,IMapper mapper, ILogger<DepartmentController> _logger, IWebHostEnvironment environment)
        {
            DepartmentServices = _DepartmentServices;
            this.mapper = mapper;
            logger = _logger;
            this.environment = environment;
        }

        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var Departments = DepartmentServices.GetAllDepartments();
            // ViewData is a Dictionary => Key , Value
            //ViewData["Message"] = "Hello From ViewData";
            //ViewBag.Message = "Hello From ViewBag";
            //ViewBag.Message = 7 ;
            //string name = ViewBag.Message ;//ViewBag is dynamic

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
        public IActionResult Create(DepartmentVM departmentVM)
        {
            //ServerSide Validation 
            if(!ModelState.IsValid)
                return View(departmentVM);

            var Message = string.Empty;
            try
            {
                //Auto Mapper
                var DepartmentDto = mapper.Map<DepartmentVM,CreatedDepartmentDto>(departmentVM);
                //var DepartmentDto = new CreatedDepartmentDto() 
                //{
                //    Name = departmentVM.Name,
                //    Code = departmentVM.Code,
                //    CreationDate = departmentVM.CreationDate,
                //    Description = departmentVM.Description,
                //};
                var result = DepartmentServices.CreateDepartment(DepartmentDto);
                if (result > 0)
                {
                    TempData["Message"] =$"{DepartmentDto.Name} Department is created ";
                    return RedirectToAction(nameof(Index));
                }
                else
                    Message = "Department is not created .";
                  
            }
            catch (Exception ex)
            {
                //Log Exception Kestral
                logger.LogError(ex, ex.Message);

                //Set Default Message For User 
                if (environment.IsDevelopment())
                    Message = ex.Message;
                else
                    Message = "An Error Occurred While Creation";
            }
            ModelState.AddModelError(string.Empty, Message);
            return View(departmentVM);
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

            var MappedDepartment = mapper.Map<DepartmentDetailsDto, DepartmentVM>(department);

            //var MappedDepartment = new DepartmentVM()
            //{
            //    Id = department.id,
            //    Name = department.Name,
            //    Code = department.Code,
            //    Description = department.Description,
            //    CreationDate = department.CreationDate
            //};

            return View(MappedDepartment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DepartmentVM departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);

            var Message = string.Empty;
            try
            {
                var departmentDto = mapper.Map<DepartmentVM, UpdatedDepartmenDto>(departmentVM);
                //var departmentDto = new UpdatedDepartmenDto()
                //{
                //        Id = departmentVM.Id,
                //        Name = departmentVM.Name,
                //        Code = departmentVM.Code,
                //        Description = departmentVM.Description,
                //        CreationDate = departmentVM.CreationDate

                //};
                var result = DepartmentServices.UpdateDepartment(departmentDto);
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

            return View(departmentVM);
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
