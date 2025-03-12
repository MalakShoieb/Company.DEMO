using Company.DEMO.BLL.Interfaces;
using Company.DEMO.BLL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Company.DEMO.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _department;//null
        //ask clr to create object mn el department repostry
        public DepartmentController( IDepartmentRepository deps)
        {
            _department = deps;
        }
        [HttpGet]//get
        
        public IActionResult Index()
        { 
            var h= _department.GetAll();
            return View(h);
        }
    }
}
