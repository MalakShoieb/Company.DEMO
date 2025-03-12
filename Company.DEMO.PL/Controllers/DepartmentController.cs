using Company.DEMO.BLL.Interfaces;
using Company.DEMO.BLL.Repository;
using Company.DEMO.DAL.Entities;
using Company.DEMO.PL.Models;
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
        [HttpGet]
        public IActionResult Create()
        {
            return View(); 
        }
        [HttpGet]

   
        public IActionResult Details(int id)
        {
            var dep = _department.GetById(id);
            if (dep == null)
            {
                return NotFound();
            }

            var model = new CreateDepartmentDTO()
            {
                Code = dep.Code,
                Name = dep.Name,
                CreateAt = dep.CreateAt


            };
            
           
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(CreateDepartmentDTO MODEL)  
        {
            if (ModelState.IsValid) //SERVER SIDE VALIDATION
            {
                var dep = new Department()
                {
                    Code = MODEL.Code,
                    Name = MODEL.Name,
                    CreateAt = MODEL.CreateAt,
                };
               var count = _department.Add(dep);
                if (count > 0) 
                {
                    return RedirectToAction("index");
                
                
                }


            }

            return View(MODEL);
        }
    }
}
