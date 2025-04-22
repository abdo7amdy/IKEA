using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Dto_s.Employees;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.BLL.Services.EmployeeServices;
using IKEA.PL.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services - DI
        private readonly IEmployeeServices employeeServices;
        private readonly ILogger<EmployeeController> logger;
        private readonly IWebHostEnvironment environment;

        public EmployeeController(IEmployeeServices employeeServices,IDepartmentServices departmentServices, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeVM EmployeeVM)
        {
            //ServerSide Validation 
            if (!ModelState.IsValid)
                return View(EmployeeVM);

            var Message = string.Empty;
            try
            {
                var DepartmentDto = new CreatedEmployeeDto()
                {
                    Name = EmployeeVM.Name,
                    Address = EmployeeVM.Address,
                    Age = EmployeeVM.Age,
                    IsActive = EmployeeVM.IsActive,
                    Salary = EmployeeVM.Salary,
                    PhoneNumber = EmployeeVM.PhoneNumber,
                    Email = EmployeeVM.Email,
                    EmpolyeeType = EmployeeVM.EmpolyeeType,
                    Gender = EmployeeVM.Gender,
                    HiringDate = EmployeeVM.HiringDate,
                };

                var result = employeeServices.CreateEmployee(DepartmentDto);
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
            return View(EmployeeVM);

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

            var MappedEmployee = new EmployeeVM()
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
        [ValidateAntiForgeryToken]

        public IActionResult Edit(EmployeeVM employeeVM)
        {
            if (!ModelState.IsValid)
                return View(employeeVM);

            var Message = string.Empty;
           
            try
            {
                var employeeDto = new UpdatedEmployeeDto()
                {
                    id = employeeVM.id,
                    Name = employeeVM.Name,
                    Address = employeeVM.Address,
                    Age = employeeVM.Age,
                    IsActive = employeeVM.IsActive,
                    Salary = employeeVM.Salary,
                    PhoneNumber = employeeVM.PhoneNumber,
                    Email = employeeVM.Email,
                    EmpolyeeType = employeeVM.EmpolyeeType,
                    Gender = employeeVM.Gender,
                    HiringDate = employeeVM.HiringDate,
                };
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

            return View(employeeVM);
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
        [ValidateAntiForgeryToken]
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
