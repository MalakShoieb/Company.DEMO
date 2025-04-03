using Company.DEMO.DAL.Data.Configuration;
using Company.DEMO.PL.Helpers;
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
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendResetPassword(ForgetPasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await _user.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var token =  await _user.GeneratePasswordResetTokenAsync (user);
                    var url = Url.Action("ResetPassword", "Account", new { email = model.Email , token }, Request.Scheme);
                    var email = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body = url
                    };
                    var flag=EmailSetting.EmailSettings(email);
                    if(flag)
                    {
                        return RedirectToAction("CheckInbox");
                    }


                }
            }

            ModelState.AddModelError("", "Invalid Email");
            return View(nameof(ForgetPassword), model);
        }
        public IActionResult CheckInbox()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email,string token)
        {
            TempData["token"] = token;
            TempData["email"] = email;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetDTO reset)
        {

            if (ModelState.IsValid)

            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;
                if (email is null || token is null)
                {
                    return BadRequest("Invalid");

                }
                var user = await _user.FindByEmailAsync(email);
                if (user is not null)
                {
                    var result = await _user.ResetPasswordAsync(user, token, reset.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("SignIn");
                    }
                }
                ModelState.AddModelError("", "Invalid please try again");



            }
            ModelState.AddModelError("", "Invalid please try again");
            return View();
        }


    }
  
}
