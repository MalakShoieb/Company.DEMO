using System.CodeDom;
using System.Runtime.Intrinsics.Arm;
using Company.DEMO.BLL.Interfaces;
using Company.DEMO.DAL.Entities;
using Company.DEMO.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.DEMO.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IemployeeRepository _iemployee;

        public EmployeeController(IemployeeRepository iemployee)
        {
            _iemployee = iemployee;
        }

        public IActionResult Index()
        {
            var employees = _iemployee.GetAll();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeDTO employeeDTO)
        {
            var emo = new Employee()
            {
                Name = employeeDTO.Name,
                Address = employeeDTO.Address,
                Age = employeeDTO.Age,
                CreatedAt = employeeDTO.CreatedAt,
                Email = employeeDTO.Email,
                IsActive = employeeDTO.IsActive,
                IsDeleted = employeeDTO.IsDeleted,
                Phone = employeeDTO.Phone,
                Salary = employeeDTO.Salary,
                StartAt = employeeDTO.StartAt,
            };
            var count = _iemployee.Add(emo);
            if (count > 0)
            {
                return RedirectToAction("Index");
            }
            return View(employeeDTO);
        }


        public IActionResult Details(int? id, string ViewName = "Details")

        {
            if (!id.HasValue || id <= 0)
            {
                return BadRequest("INVALID ID");
            }
           
            var employees = _iemployee.GetById(id.Value);

            if (employees == null)
            {
                return NotFound(new { StatusCode = "400", message = $"Department with {id} is not found" });
            }
            return View(ViewName, employees);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {


            return Details(id, "Edit");
        }
   
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, EmployeeDTO model)
        {
            if (ModelState.IsValid)
            {
                var emp = new Employee()
                {
                    Id = id,
                    Name = model.Name,
                    Email = model.Email,
                    Salary = model.Salary,
                    Address = model.Address,
                    Age = model.Age,
                    CreatedAt = model.CreatedAt,
                    StartAt = model.StartAt,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    Phone = model.Phone
                };

                var count = _iemployee.Update(emp);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to update the employee. Please try again.");
                }
            }

            return View(model); 
        }
        public IActionResult Delete(int id)
        {
            var emp=_iemployee.GetById(id);
            var em=_iemployee.Delete(emp);
            return RedirectToAction("Index");

            
        }


    }
}