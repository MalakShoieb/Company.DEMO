using System.CodeDom;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using AutoMapper;
using AutoMapper.Features;
using Company.DEMO.BLL.Interfaces;
using Company.DEMO.BLL.Repository;
using Company.DEMO.DAL.Entities;
using Company.DEMO.PL.Helpers;
using Company.DEMO.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Company.DEMO.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        //private readonly IemployeeRepository _iemployee;
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(/*IemployeeRepository iemployee*/IUnitOfWork unitOfWork  , /*IDepartmentRepository departmentRepository*/IMapper mapper)
        {
         _unitOfWork = unitOfWork;
            //_iemployee = iemployee;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string? search)
        {
            IEnumerable<Employee> employees1;
            if (string.IsNullOrEmpty(search))
            {
                employees1 =await _unitOfWork.EmployeeRepository.GetAllAsync();
               
            }
            else
            {
                employees1 =await _unitOfWork.EmployeeRepository.GetByNameAsync(search);
            }

            ViewBag.Message = "Hello FROM viewBag";
            return View(employees1);
        }
        [HttpGet]
        public  async Task<IActionResult> Create()
        {
            //var department = _unitOfWork.DepartmentRepository.GetAllAsync();
            //ViewData["department"] = department;
            //ViewBag.Departments = new SelectList(departments, "Id", "Name")
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync(); // Await the method properly
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Create(EmployeeDTO employeeDTO)

        {
            if (ModelState.IsValid)
            {
                //var emo = new Employee()
                //{
                //    Name = employeeDTO.Name,
                //    Address = employeeDTO.Address,
                //    Age = employeeDTO.Age,
                //    CreatedAt = employeeDTO.CreatedAt,
                //    Email = employeeDTO.Email,
                //    IsActive = employeeDTO.IsActive,
                //    IsDeleted = employeeDTO.IsDeleted,
                //    Phone = employeeDTO.Phone,
                //    Salary = employeeDTO.Salary,
                //    StartAt = employeeDTO.StartAt,
                //    DepartmentId = employeeDTO.DepartmentId,
                //};
                if(employeeDTO.Image != null)
                {
                    employeeDTO.Imagenames = DocumentSettings.Upload(employeeDTO.Image, "Images");
                }
                var emo = _mapper.Map<Employee>(employeeDTO);

                await _unitOfWork.EmployeeRepository.AddAsync(emo);
                var count =await _unitOfWork.EmployeeRepository.Complete();

                if (count > 0)
                {
                    TempData["message"] = "Employee is Added !!!";
                    return RedirectToAction("Index");
                }
               
            }
            //var department = _departmentRepository.GetAll();
            //ViewData["department"] = department ?? new List<Department>();
            return View(employeeDTO);
        }


        public async Task<IActionResult> Details(int? id,string ViewName = "Details")

        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync(); // Await the method properly
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            if (!id.HasValue || id <= 0)
            {
                return BadRequest("INVALID ID");
            }

            var employees =await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            var model = _mapper.Map<EmployeeDTO>(employees);


            if (employees == null)
            {
                return NotFound(new { StatusCode = "400", message = $"Department with {id} is not found" });
            }
            return View(ViewName, model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)

        {
            //var department = _departmentRepository.GetAll();
            //ViewData["department"] = department;
            

            if (id == 0) { return BadRequest(); };
            var emp =await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            if (emp == null)
            {
                return NotFound(new { StatusCode = "400" });
            }
            //var employeeDTO = new EmployeeDTO
            //{
            //    Name = emp.Name,
            //    Address = emp.Address,
            //    Age = emp.Age,
            //    CreatedAt = emp.CreatedAt,
            //    Email = emp.Email,
            //    IsActive = emp.IsActive,
            //    IsDeleted = emp.IsDeleted,
            //    Phone = emp.Phone,
            //    Salary = emp.Salary,
            //    StartAt = emp.StartAt,
            //    DepartmentId = emp.DepartmentId
            //};
            var model = _mapper.Map<EmployeeDTO>(emp);


            return View(model);




        }

        //[HttpPost]
        //public IActionResult Edit([FromRoute] int id, Employee model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //         if (id != model.Id) { return BadRequest(); };
        //        var emp = _iemployee.Update(model);

        //        if (emp > 0)
        //        {
        //            TempData["message"] = "Employee is update !";
        //            return RedirectToAction("Index");
        //        }

        //    }

        //    return View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeDTO model)
        {
            if (ModelState.IsValid)
            {
                if(model.Imagenames is not null&& model.Image is not null)
                {
                    DocumentSettings.Delete("Images", model.Imagenames);
                }
                if (model.Image != null)
                {
                    model.Imagenames = DocumentSettings.Upload(model.Image, "Images");
                }
                var emo = new Employee()
                {Id=id,
                    Name = model.Name,
                    Address = model.Address,
                    Age = model.Age,
                    CreatedAt = model.CreatedAt,
                    Email = model.Email,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    Phone = model.Phone,
                    Salary = model.Salary,
                    StartAt = model.StartAt,
                    DepartmentId = model.DepartmentId,
                    ImageNames=model.Imagenames
                };

                //if (id != model.Id) { return BadRequest(); };
                 _unitOfWork.EmployeeRepository.Update(emo);
                var emp = await _unitOfWork.EmployeeRepository.Complete();

                if (emp > 0)
                {
                    TempData["message"] = "employee is update !";
                    return RedirectToAction("index");
                }

            }

            return View(model);
        }
        //public IActionResult Delete(int id)
        //{
        //    var emp = _iemployee.GetById(id);
        //    var em = _iemployee.Delete(emp);
        //    if (em > 0)
        //    {
        //        TempData["message"] = "Employee is deleted !";
        //    }
        //    return RedirectToAction("Index"); }

        //public IActionResult Delete(int id)
        //{
        //    var emp = _iemployee.GetById(id);

        //    var em = _iemployee.Delete(emp);
        //} 


        //}
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            //var department= _departmentRepository.GetAll();
            //ViewData["department"] = department;
            
             var emp = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            var model = _mapper.Map<EmployeeDTO>(emp);

            return View(model);
        }
        [HttpPost]
        //public IActionResult Delete([FromRoute] int id, Employee employee)
        //{
        //    var model = _iemployee.GetById(id);
        //    var result = _iemployee.Delete(model);
        //    if (result > 0)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View(model);
        [HttpPost]
            public async  Task<IActionResult> Delete(int id,  EmployeeDTO model)
            { var department=_mapper.Map<Employee>(model);
            department.Id = id;


            _unitOfWork.EmployeeRepository.Delete(department);
            var count =await _unitOfWork.EmployeeRepository.Complete();
                if (count > 0)
            {
                if ( model.Imagenames  is not null )
                {
                    DocumentSettings.Delete("Images", model.Imagenames);
                }

                return RedirectToAction("index");
                }
                return View(department);
            }





        }
    } 