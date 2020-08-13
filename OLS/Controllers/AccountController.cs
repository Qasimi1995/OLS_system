using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OLS.ViewModels;
using OLS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EmailService;

namespace OLS.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext _applicationContext;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(ApplicationContext applicationContext,UserManager<User> userManager,ILogger<AccountController> logger,SignInManager<User> signInManager, IEmailSender emailSender) {

            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _applicationContext = applicationContext;


        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RessetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RessetPassword( RessetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if(user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        ViewBag.Message = "Password Recoverd Successfully";
                        return View("ResetPasswordConfirmation");
                        
                    }
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
                ViewBag.Message = "Password Recoverd Successfully";
                return View("ResetPasswordConfirmation");
                
            }
            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword (ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if(user != null)// if you want to send to confirmed email && await _userManager.IsEmailConfirmedAsync(user)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("RessetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);

                    var message = new Message(new string[] { model.Email }, "Password Reset ", "Hey Dear Please use the following link to reset your password.  if you did not request this password reset please ignore this  email. Token time for reset password is 2 hours please reset password within the token time range" + "\n" + passwordResetLink );
                    _emailSender.SendEmail(message);

                    //  _logger.Log(LogLevel.Warning, passwordResetLink);
                    ViewBag.Message = "Successfully Sent the Reset Password Link To your Email";
                    return View("ForgotPasswordConfirmation");
                }
                else
                {
                    ViewBag.Message = "Enterd Email address not exist please confirm if its not wrong";
                    return View("ForgotPasswordConfirmationError");
                }
                
            }

            return View(model);
        }

    
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpGet]
        public IActionResult changePasswordConfirmation() {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid) {

                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {

                    return RedirectToAction("Login");
                }

                var result = await _userManager.ChangePasswordAsync(user, changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);

                if (!result.Succeeded)
                {

                    foreach (var error in result.Errors)
                    {

                        ModelState.AddModelError(string.Empty, error.Description);

                    }

                    return View();

                }
                await _signInManager.RefreshSignInAsync(user);
                ViewBag.Message = "Password Updated successfully";
                return View("ChangePassword");
            }

            return View(changePasswordViewModel);
        }


        [HttpGet]
        public IActionResult EditUserInfo(string Id)
        {
            try
            {
                var province = _applicationContext.ZProvince;
                ViewBag.province = province;

                ViewBag.roles = _applicationContext.Roles.Where(p =>p.Name != "Applicant").ToList();
                if (Id != null)
                {
                    var userInfo = (from user in _applicationContext.Users
                                join userrole in _applicationContext.UserRoles on user.Id equals userrole.UserId
                                join role in _applicationContext.Roles on userrole.RoleId equals role.Id
                                where user.Id == Id
                                    select new UserViewModel
                                {
                                    Id = user.Id,
                                    FirstName = user.FirstName,
                                    LastName = user.LastName,
                                    Email = user.Email,
                                    ProvinceId=user.ProvinceId,
                                    IsActive=user.IsActive,
                                    
                                }).FirstOrDefault();

                    return View(userInfo);
                }
                else
                {
                    return View("index");

                }


            }
            catch (Exception ex)
            {

                return View("Index");
            }
        }

        [HttpPost]
        public IActionResult EditUserInfo(UserViewModel userViewModel)
        {
           
            try
            {
                
                    var user = _applicationContext.Users.Where(p => p.Id == userViewModel.Id).FirstOrDefault();
                    user.FirstName = userViewModel.FirstName;
                    user.LastName = userViewModel.LastName;
                    user.Email = userViewModel.Email;
                    user.ProvinceId = userViewModel.ProvinceId;
                    user.IsActive = userViewModel.IsActive;

                    _applicationContext.Update(user);
                    _applicationContext.SaveChanges();

                    ViewBag.Message = "Record Updated Successfully";
                    //return RedirectToAction("EditUserInfo");
                
                    return View(userViewModel);



            }

            catch (Exception ex)
            {

                var province = _applicationContext.ZProvince;
                ViewBag.province = province;

                ViewBag.roles = _applicationContext.Roles.Where(p => p.Name != "Applicant").ToList();
                return View(userViewModel);
            }
        }


        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        public IActionResult RegisterStaff()
        {
            ViewBag.userList = (from user in _applicationContext.Users
                            join userrole in _applicationContext.UserRoles on user.Id equals userrole.UserId
                            join role in _applicationContext.Roles on userrole.RoleId equals role.Id
                                where role.Name !="Applicant"
                            select new UserViewModel
                            {
                                Id = user.Id,
                                UserName = user.UserName,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                Role = role.Name,
                                Email=user.Email,
                                IsActive=user.IsActive,
                            }).ToList();

            ViewBag.roles = _applicationContext.Roles.ToList();
            var province = _applicationContext.ZProvince;
            ViewBag.province = province;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStaff(RegistrationStaffViewModel model)
        {
            ViewBag.roles = _applicationContext.Roles.ToList();
            var province = _applicationContext.ZProvince;
            ViewBag.province = province;
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ProvinceId=model.ProvinceId,
                    IsActive=model.IsActive,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                var addedToRole = await _userManager.AddToRoleAsync(user, model.RoleId);

                if (result.Succeeded && addedToRole.Succeeded)
                {
                    //return RedirectToAction("RegisterStaff");
                    ViewBag.Message = "Sucessful";
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.TryAddModelError("", error.Description);
                    }

                }
            }


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var user = new User {
#pragma warning disable CA1062 // Validate arguments of public methods
                    UserName = model.UserName,
#pragma warning restore CA1062 // Validate arguments of public methods
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    IsActive = model.IsActive = 1
                
                };
              var result= await _userManager.CreateAsync(user,model.Password).ConfigureAwait(true);
              var addedToRole = await _userManager.AddToRoleAsync(user, "APPLICANT").ConfigureAwait(true);
                
                if (result.Succeeded && addedToRole.Succeeded)
                {
                    //var TheUser = _applicationContext.Users.Where(p => p.UserName == model.UserName).FirstOrDefault();
                    //TheUser.IsActive = 1;
                    //_applicationContext.Update(TheUser);
                    //_applicationContext.SaveChanges();

                    ViewBag.Message = "Account Created";
                    return View(model);
                }
                else {
                    foreach (var error in result.Errors) {
                        ModelState.TryAddModelError("",error.Description);
                    }
                
                }
            }
           

            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) {

            if (ModelState.IsValid) {
            var result=   await _signInManager.PasswordSignInAsync(model.UserName,model.Password,model.RememberMe,false);
            var IsActive = _applicationContext.Users.Where(p => p.UserName==model.UserName).Select(p => p.IsActive).FirstOrDefault();
                //var Role = (from user in _applicationContext.Users
                //            join roles in _applicationContext.UserRoles on user.Id equals roles.RoleId
                //            select new { roles.RoleId });
                if (result.Succeeded)
                {
                    if (IsActive == 1)
                    {

                        if (Request.Query.Keys.Contains("ReturnUrl"))
                        {
                          
                            return Redirect(Request.Query["ReturnUrl"].FirstOrDefault());
                        }
                        else
                        {


                            return RedirectToAction("Navigate", "Home");
                        }

                    }
                    else {

                        ModelState.TryAddModelError("", "Account is inactive, please contact administrator");
                    }

                }
                else {

                    ModelState.TryAddModelError("", "نام کاربر و یا رمز عبور اشتباه است / کارن نوم یا رمز غلط دی");
                }
            }

            return View();
        }


        public async Task<IActionResult> Logout() {

            if (User.Identity.IsAuthenticated) {
               await _signInManager.SignOutAsync();
            
            }
            return RedirectToAction("Login","Account");
        }


    }
}