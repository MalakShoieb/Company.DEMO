using AutoMapper;
using Company.DEMO.BLL.Interfaces;
using Company.DEMO.BLL.Repository;
using Company.DEMO.DAL.Entities;
using Company.DEMO.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Company.DEMO.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        /*private readonly IDepartmentRepository _department*///null
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        //ask clr to create object mn el department repostry
        public DepartmentController(/*IDepartmentRepository deps*/IMapper mapper,IUnitOfWork unitOfWork)
        {
            //_department = deps;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]//get
        public async Task<IActionResult> Index()
        {
            var h = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(h);
        }
        [HttpGet]
        public async Task<IActionResult>  Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string ViewName="Details")
        {
            if (id == 0) return BadRequest("Invalid ID");
            var dep =  await _unitOfWork.DepartmentRepository.GetByIdAsync(id.Value);
            if (dep == null)
            {
                return NotFound(new { StatusCode = "400", message = $"Department with {id} is not found" });
            }
            return View(ViewName, dep);
        }

        [HttpPost]
        public  async Task<IActionResult> Create(CreateDepartmentDTO MODEL)
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
                
                    await _unitOfWork.DepartmentRepository.AddAsync(dep);
                var count =await _unitOfWork.DepartmentRepository.Complete();
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(MODEL);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //prevent usage outside browser
        public async Task<IActionResult> Edit([FromRoute] int id, CreateDepartmentDTO MODEL)
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
                _unitOfWork.DepartmentRepository.Update(dep);
                var count = await _unitOfWork.DepartmentRepository.Complete();
                if (count > 0)
                {
                    return RedirectToAction("index");
                }
            }
            return View(MODEL);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, Department department)
        {
            _unitOfWork.DepartmentRepository.Delete(department);
             var count =await  _unitOfWork.DepartmentRepository.Complete();
            if (count > 0)
            {
                return RedirectToAction("index");
            }
            return View(department);
        }
    }
}