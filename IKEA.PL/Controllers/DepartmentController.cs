using IKEA.BLL.Services.DepartmentServices;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentServices DepartmentServices;
        public DepartmentController(IDepartmentServices _DepartmentServices)
        {
            DepartmentServices = _DepartmentServices;
        }
        #region Index
        public IActionResult Index()
        {
            var Departments = DepartmentServices.GetAllDepartments();
            return View(Departments);
        } 
        #endregion
    }
}
