using Company.DEMO.DAL.Data.Configuration;
using Company.DEMO.DAL.Entities;
using Company.DEMO.PL.Helpers;
using Company.DEMO.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Company.DEMO.PL.Controllers
{
    public class RoleController : Controller
    {

        private readonly RoleManager<IdentityRole> _role;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> role, UserManager<AppUser> userManager)
        {

            _role = role;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string? search)
        {
            IEnumerable<RoleDTO> user;
            if (string.IsNullOrEmpty(search))
            {
                user = _role.Roles.Select(u => new RoleDTO
                {
                    ID = u.Id,
                    Name = u.Name

                });

            }
            else
            {
                user = _role.Roles.Select(u => new RoleDTO
                {
                    ID = u.Id,
                    Name = u.Name

                }).Where(u => u.Name.ToLower().Contains(search.ToLower()));



            }
            return View(user);
        }
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")

        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("INVALID ID");
            }
            ViewData["id"] = id;
            var user = await _role.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new RoleDTO
            {
                ID = user.Id,
                Name = user.Name

            };



            return View(ViewName, model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)

        {

            if (id is null) { return BadRequest(); };
           
            var emp = await _role.FindByIdAsync(id);
            if (emp == null)
            {
                return NotFound(new { StatusCode = "400" });
            }
            var model = new RoleDTO
            {
                ID = emp.Id,
                Name = emp.Name
            };


            ViewData["id"] = id;

            return View(model);




        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleDTO model)
        {
            if (ModelState.IsValid)
            {
                var em = await _role.FindByIdAsync(id);
                if (em == null)
                {
                    return NotFound(new { StatusCode = "400" });
                }
                em.Id = model.ID;
                em.Name = model.Name;

                var result = await _role.UpdateAsync(em);
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

            var emp = _role.FindByIdAsync(id).Result;
            if (emp == null)
            {
                return NotFound();
            }

            var result = _role.DeleteAsync(emp).Result;
            if (result.Succeeded)
            {
                TempData["message"] = "role is deleted!";
            }
            else
            {
                TempData["error"] = "Error deleting role!";
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleDTO roleDTO)

        {
            if (ModelState.IsValid)
            {
                var emp = await _role.FindByNameAsync(roleDTO.Name);
                if (emp is not null)
                { return BadRequest("Already exist"); }
                var role = new IdentityRole
                {

                    Name = roleDTO.Name
                };
                var result = await _role.CreateAsync(role);
                if (result.Succeeded)
                {
                    TempData["message"] = "Role is Added !!!";
                    return RedirectToAction("Index");
                }

            }
            return View(roleDTO);

        }
        [HttpGet]

        public async Task<IActionResult> AddorRemove(string RoleId)
        {

            var role = await _role.FindByIdAsync(RoleId);

            if (role == null)
            {
                return NotFound(new { StatusCode = "400" });
            }
               ViewData["Role"] = RoleId;
            var UserInrole = new List<AddOrRemove>();
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var userInrole = new AddOrRemove()
                {
                    UserId = user.Id,
                    UserName = user.UserName,

                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userInrole.IsSelected = true;
                }
                else
                {
                    userInrole.IsSelected = false;
                }
                UserInrole.Add(userInrole);




            }
            return View(UserInrole);


        }
        [HttpPost]
        public async Task<IActionResult> AddorRemove(string RoleId, List<AddOrRemove> users)
        {
            if (ModelState.IsValid)
            {
                var role = await _role.FindByIdAsync(RoleId);
                if (role is null)
                {
                    return BadRequest();
                }


                foreach (var x in users)
                {
                    var app = await _userManager.FindByIdAsync(x.UserId);
                    if (app is not null)
                    {
                        if (x.IsSelected && !await _userManager.IsInRoleAsync(app, role.Name))
                        {
                            await _userManager.AddToRoleAsync(app, role.Name);
                        }
                        else if (!x.IsSelected && await _userManager.IsInRoleAsync(app, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(app, role.Name);
                        }
                    }

                }
                return RedirectToAction(nameof(Edit), new { id = RoleId });
            }
            return View(users);
        }


        //[HttpPost]
        //public async Task<IActionResult> AddorRemove(string RoleId, List<AddOrRemove> users)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    if (users == null)
        //    {
        //        return BadRequest("User list cannot be null.");
        //    }

        //    var role = await _role.FindByIdAsync(RoleId);
        //    if (role is null)
        //    {
        //        return BadRequest();
        //    }

        //    foreach (var x in users)
        //    {
        //        var app = await _userManager.FindByIdAsync(x.UserId);
        //        if (app is not null)
        //        {
        //            if (x.IsSelected && !await _userManager.IsInRoleAsync(app, role.Name))
        //            {
        //                await _userManager.AddToRoleAsync(app, role.Name);
        //            }
        //            else if (!x.IsSelected && await _userManager.IsInRoleAsync(app, role.Name))
        //            {
        //                await _userManager.RemoveFromRoleAsync(app, role.Name);
        //            }
        //        }
        //    }
        //    return RedirectToAction(nameof(Edit), new { id = RoleId });
        //}






    }
}


    