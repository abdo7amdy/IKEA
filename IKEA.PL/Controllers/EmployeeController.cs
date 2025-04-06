using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Dto_s.Employees;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.BLL.Services.EmployeeServices;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services - DI
        private readonly IEmployeeServices employeeServices;
        private readonly ILogger<EmployeeController> logger;
        private readonly IWebHostEnvironment environment;

        public EmployeeController(IEmployeeServices employeeServices, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            this.employeeServices = employeeServices;
            this.logger = logger;
            this.environment = environment;
        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var employees = employeeServices.GetAllEmployees();
            return View(employees);
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = employeeServices.GetEmployeeById(id.Value);

            if (employee is null)
                return NotFound();

            return View(employee);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto EmployeeDto)
        {
            //ServerSide Validation 
            if (!ModelState.IsValid)
                return View(EmployeeDto);

            var Message = string.Empty;
            try
            {
                var result = employeeServices.CreateEmployee(EmployeeDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
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
            return View(EmployeeDto);

        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id is null)
                return BadRequest();
            var employee = employeeServices.GetEmployeeById(Id.Value);

            if (employee == null)
                return NotFound();

            var MappedEmployee = new UpdatedEmployeeDto()
            {
                id = employee.id,
                Name = employee.Name,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber, 
                Address = employee.Address,
                HiringDate = employee.HiringDate,
                Salary = employee.Salary,
                Gender = employee.Gender,
                EmpolyeeType = employee.EmpolyeeType,
                IsActive = employee.IsActive
            };
            return View(MappedEmployee);
        }

        [HttpPost]
        public IActionResult Edit(UpdatedEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return View(employeeDto);

            var Message = string.Empty;
            try
            {
                var result = employeeServices.UpdateEmployee(employeeDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    Message = "Employee Is Not Updated ..!";

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                Message = environment.IsDevelopment() ? ex.Message : "An Error Has Been Occurred During Updating Employee  ";
                throw;
            }

            ModelState.AddModelError(string.Empty, Message);

            return View(employeeDto);
        }


        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null) return BadRequest();
            var employee = employeeServices.GetEmployeeById(Id.Value);
            if (employee is null) return NotFound();

            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete(int EmpId)
        {
            var Message = string.Empty;

            try
            {
                var IsDeleted = employeeServices.DeleteEmployee(EmpId);
                if (IsDeleted) return RedirectToAction(nameof(Index));

                Message = " Employee Is Not Deleted .";

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                Message = environment.IsDevelopment() ? ex.Message : "An Error Has Been Occurred During Deleteing Employee";
                throw;
            }

            ModelState.AddModelError(string.Empty, Message);
            return RedirectToAction(nameof(Delete), new { id = EmpId });

        }
        #endregion




    }
}
