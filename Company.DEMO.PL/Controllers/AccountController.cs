using Company.DEMO.DAL.Data.Configuration;
using Company.DEMO.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.DEMO.PL.Controllers
{
    public class AccountController : Controller

    {
        private readonly UserManager<AppUser> _user;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> user,SignInManager<AppUser>signInManager)
        {
            _user = user;
           _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult SignUp()//Get: ACCOUNT/SignUp
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(AppUserDto model)

        {
            if (ModelState.IsValid)
            {
                var user = await _user.FindByNameAsync(model.UserName);
                if (user is null)
                {
                    user = await _user.FindByEmailAsync(model.Email);
                    if (user is null)
                    {
                        user = new AppUser()
                        {
                            UserName = model.UserName,
                            Email = model.Email,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            IsAgree = model.IsAgree



                        };
                        var result = await _user.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("SignIn");
                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }

                }
                ModelState.AddModelError("", "Invaild sign in");

            }
            return View(model);

        }
        [HttpGet]
        //Passw@rd123
        public IActionResult SignIn()//Get: ACCOUNT/SignIn
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDTO model)
        {
            if(ModelState.IsValid)
            {
                var user= await _user.FindByEmailAsync(model.Email);
                if(user is not null) 
                {
                    var result = await _user.CheckPasswordAsync(user, model.Password);
                    {
                        if (result)
                        {
                            var flag = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                            return RedirectToAction(nameof(HomeController.Index), "Home");

                        }

                    }
                   
                }
                    ModelState.AddModelError("", "Invalid");


                            

            }
            return View(model);
        }
        [HttpGet]
        public new async Task<IActionResult> SignOut()
        {

            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

    }
  
}
