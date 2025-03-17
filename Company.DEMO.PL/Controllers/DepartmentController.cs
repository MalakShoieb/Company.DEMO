using Company.DEMO.BLL.Interfaces;
using Company.DEMO.BLL.Repository;
using Company.DEMO.DAL.Entities;
using Company.DEMO.PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Company.DEMO.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _department;//null
        //ask clr to create object mn el department repostry
        public DepartmentController(IDepartmentRepository deps)
        {
            _department = deps;
        }
        [HttpGet]//get
        public IActionResult Index()
        {
            var h = _department.GetAll();
            return View(h);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int? id, string ViewName="Details")
        {
            if (id == 0) return BadRequest("Invalid ID");
            var dep = _department.GetById(id.Value);
            if (dep == null)
            {
                return NotFound(new { StatusCode = "400", message = $"Department with {id} is not found" });
            }
            return View(ViewName, dep);
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
                // Here you should add the department to the database
                var count =_department.Add(dep);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(MODEL);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //prevent usage outside browser
        public IActionResult Edit([FromRoute] int id, CreateDepartmentDTO MODEL)
        {
            if (ModelState.IsValid)
            {
                var dep = new Department()
                {
                    Id = id,
                    Name = MODEL.Name,
                    CreateAt = MODEL.CreateAt,
                    Code = MODEL.Code,
                };
                var count = _department.Update(dep);
                if (count > 0)
                {
                    return RedirectToAction("index");
                }
            }
            return View(MODEL);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(int id, Department department)
        {
            var count = _department.Delete(department);
            if (count > 0)
            {
                return RedirectToAction("index");
            }
            return View(department);
        }
    }
}