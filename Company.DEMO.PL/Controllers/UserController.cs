using Company.DEMO.DAL.Data.Configuration;
using Company.DEMO.DAL.Entities;
using Company.DEMO.PL.Helpers;
using Company.DEMO.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Company.DEMO.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string? search)
        {
            IEnumerable<USERSdtocs> user;
            if (string.IsNullOrEmpty(search))
            {
                user = _userManager.Users.Select(u => new USERSdtocs
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Roles = _userManager.GetRolesAsync(u).Result
                });

            }
            else
            {
                user = _userManager.Users.Select(u => new USERSdtocs
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Roles = _userManager.GetRolesAsync(u).Result
                }).Where(u => u.FirstName.ToLower().Contains(search.ToLower()));


               
            }
            return View(user);
        }
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")

        {if(string.IsNullOrEmpty(id))
            {
                return BadRequest("INVALID ID");
            }
            ViewData["id"] = id;

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new USERSdtocs
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };



            return View(ViewName, model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)

        {
 
            if (id is null) { return BadRequest(); };
            var emp = await _userManager.FindByIdAsync(id);
            if (emp == null)
            {
                return NotFound(new { StatusCode = "400" });
            }
            var model = new USERSdtocs
            {
                Id = emp.Id,
                UserName = emp.UserName,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Email = emp.Email,
                Roles = _userManager.GetRolesAsync(emp).Result
            };

            ViewData["id"] = id;

            return View(model);




        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string id, USERSdtocs model)
        {
            if (ModelState.IsValid)
            { var em = await _userManager.FindByIdAsync(id);
                if (em == null)
                {
                    return NotFound(new { StatusCode = "400" });
                }
                em.UserName = model.UserName;
                em.FirstName = model.FirstName;
                em.LastName = model.LastName;
                em.Email = model.Email;
                var result = await _userManager.UpdateAsync(em);
                if (result.Succeeded)
                {
                    TempData["message"] = "User is update !";
                    return RedirectToAction("index");
                }

            }

            return View(model);
        }
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("INVALID ID");
            }

            var emp = _userManager.FindByIdAsync(id).Result;
            if (emp == null)
            {
                return NotFound();
            }

            var result = _userManager.DeleteAsync(emp).Result;
            if (result.Succeeded)
            {
                TempData["message"] = "user is deleted!";
            }
            else
            {
                TempData["error"] = "Error deleting user!";
            }

            return RedirectToAction("Index");
        }
    }
}
