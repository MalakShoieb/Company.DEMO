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
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IemployeeRepository iemployee,IDepartmentRepository departmentRepository)
        {
            _iemployee = iemployee;
           _departmentRepository = departmentRepository;
        }

        public IActionResult Index()
        {

            //var dep=_departmentRepository.GetAll();
            var employees = _iemployee.GetAll();
            //VIEW => DICTIONARY =>[kEY,Value]
            //ACESS => VIEW 
            /*
             1)ViewDATA => tranfer extra info -> action -> view
            2) ViewBag =>tranfer extra info -> action -> view
            3)TempData => per Request 
            
            */
            //ViewData["Message"] = "Hello FROM viewDatas";
            ViewBag.Message = "Hello FROM viewBag";
            /*ViewBag.Message = new { message="Hello from anaoynumos TYPE" }*/
            ;
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeDTO employeeDTO)
        {if (ModelState.IsValid)
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
                    StartAt = employeeDTO.StartAt ,
                    DepartmentId = employeeDTO.DepartmentId,
                };
                var count = _iemployee.Add(emo);
                if (count > 0)
                {
                    TempData["message"] = "Employee is Added !!!";
                    return RedirectToAction("Index");
                }
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
            if (id == 0) { return BadRequest(); };
            var emp=_iemployee.GetById(id.Value);
            if (emp == null)
            {
                return NotFound(new { StatusCode = "400" });
            }
            var model = new EmployeeDTO()
            {
               
                Name = emp.Name,
                Email =emp.Email,
                Salary = emp.Salary,
                Address = emp.Address,
                Age = emp.Age,
                CreatedAt = emp.CreatedAt,
                StartAt = emp.StartAt,
                IsActive = emp.IsActive,
                IsDeleted = emp.IsDeleted,
                Phone = emp.Phone
            };

            return View(model);



            
        }
   
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, EmployeeDTO  model)
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
                    TempData["message"] = "Employee is update !";
                    return RedirectToAction("Index");
                }
              
            }

            return View(model); 
        }
        public IActionResult Delete(int id)
        {
            var emp=_iemployee.GetById(id);
            var em=_iemployee.Delete(emp);
            if (em > 0)
            {
                TempData["message"] = "Employee is deleted !";
            }
            return RedirectToAction("Index");
        //public IActionResult Delete(int id)
        //{
        //    var emp=_iemployee.GetById(id);
        //    var em=_iemployee.Delete(emp);
        //    return RedirectToAction("Index");


        //}
        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        { 
                var emp = _iemployee.GetById
                    (id);
            
            return View(emp);
        }
        [HttpPost]
        //public IActionResult Delete([FromRoute] int id, Employee employee)
        //{
        //    var model=_iemployee.GetById (id);
        //    var result = _iemployee.Delete(model);
        //    if (result > 0)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View(model);
        public IActionResult Delete(int id,  Employee department)
        {
            var count = _iemployee.Delete(department);
            if (count > 0)
            {
                return RedirectToAction("index");
            }
            return View(department);
        }





    }
}