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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Localization;

namespace OLS.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext _applicationContext;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;

        private readonly IHtmlLocalizer _localizer;
    

    public AccountController(ApplicationContext applicationContext,IHtmlLocalizer<AccountController> localizer,UserManager<User> userManager,ILogger<AccountController> logger,SignInManager<User> signInManager, IEmailSender emailSender) {

            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _applicationContext = applicationContext;
            _localizer = localizer;
            

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RessetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", _localizer["InvalidPasswordResetToken"].Value);
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
                        ViewBag.Message = _localizer["PasswordRecoveredSuccessfully"].Value;
                        return View("ResetPasswordConfirmation");
                        
                    }
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
                ViewBag.Message = _localizer["PasswordRecoveredSuccessfully"].Value;
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

                    var message = new Message(new string[] { model.Email }, _localizer["PasswordReset"].Value, _localizer["PasswordResetMessage"].Value + "\n" + passwordResetLink );
                    _emailSender.SendEmail(message);

                    //  _logger.Log(LogLevel.Warning, passwordResetLink);
                    ViewBag.Message = _localizer["SuccessfullSentMessage"].Value;
                    return View("ForgotPasswordConfirmation");
                }
                else
                {
                    ViewBag.Message = _localizer["EmailNotExist"].Value;
                    return View("ForgotPasswordConfirmationError");
                }
                
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
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
                ViewBag.Message = _localizer["PasswordUpdatedSuccessfully"];
                return View("ChangePassword");
            }

            return View(changePasswordViewModel);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditUserInfo(string Id)
        {
            try
            {
               
                if (Id != null)
                {
                    var userInfo = (from user in _applicationContext.Users
                                join userrole in _applicationContext.UserRoles on user.Id equals userrole.UserId
                                join role in _applicationContext.Roles on userrole.RoleId equals role.Id
                                where user.Id == Id
                                select new UserViewModel
                                {
                                    Id = user.Id,
                                    UserName=user.UserName,
                                    FirstName = user.FirstName,
                                    LastName = user.LastName,
                                    Email = user.Email,
                                    ProvinceId=user.ProvinceId,
                                    IsActive=user.IsActive,
                                    RoleId=role.Id,
                                    Role=role.Name,
                                   
                                   
                              
                                    
                                }).FirstOrDefault();
                    var province = _applicationContext.ZProvince;
                    ViewBag.province = province;
                    ViewBag.roles = _applicationContext.Roles.Where(p => p.Name != "Applicant").ToList();
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUserInfo(UserViewModel userViewModel)
        {
            try
            {
               //for the role revocation it used
                var user = _applicationContext.Users.Where(p => p.Id == userViewModel.Id).FirstOrDefault();
                var userof = await _userManager.FindByIdAsync(user.Id);
                var currentUserRole = await _userManager.GetRolesAsync(user).ConfigureAwait(true);


                    
                    user.UserName = userViewModel.UserName;
                    user.FirstName = userViewModel.FirstName;
                    user.LastName = userViewModel.LastName;
                    user.Email = userViewModel.Email;
                    user.ProvinceId = userViewModel.ProvinceId;
                    user.IsActive = userViewModel.IsActive;
                    user.UpdatedBy = _userManager.GetUserId(User);
                    user.UpdatedAt = DateTime.Now;
                //used to check weather the the role for user get change
                if (currentUserRole[0] != userViewModel.Role)
                {
                    var removeRole = await _userManager.RemoveFromRoleAsync(user, currentUserRole[0]).ConfigureAwait(true);
                    var AddRole = await _userManager.AddToRoleAsync(user, userViewModel.Role).ConfigureAwait(true);
                }
                _applicationContext.Entry(user).State = EntityState.Modified;
                _applicationContext.Update(user);
                _applicationContext.SaveChanges();
                
                ViewBag.Message = _localizer["RecordUpdatedSuccessfully"];
                //return RedirectToAction("EditUserInfo");

                var province = _applicationContext.ZProvince;
                ViewBag.province = province;
                ViewBag.roles = _applicationContext.Roles.Where(p => p.Name != "Applicant").ToList();
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

        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
                    CreatedAt=DateTime.Now,
                    CreatedBy=_userManager.GetUserId(User)
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                var addedToRole = await _userManager.AddToRoleAsync(user, model.RoleId);

                if (result.Succeeded && addedToRole.Succeeded)
                {
                    //return RedirectToAction("RegisterStaff");
                    ViewBag.Message = _localizer["Successful"].Value;
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
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ChangeUserStatus(Guid userId)
        {

            var user = _applicationContext.Users.Where(p => p.Id == userId.ToString()).FirstOrDefault();




            if (user.IsActive == 0)
            {
                user.IsActive = 1;

            }
            else
            {
                user.IsActive = 0;

            }





            _applicationContext.Entry(user).State = EntityState.Modified;
            _applicationContext.Update(user);
            _applicationContext.SaveChanges();

            return RedirectToAction("RegisterStaff");
        }


        [HttpPost]
        [AllowAnonymous]
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
              var addedToRole = await _userManager.AddToRoleAsync(user, "Applicant").ConfigureAwait(true);
                
                if (result.Succeeded && addedToRole.Succeeded)
                {
                    //var TheUser = _applicationContext.Users.Where(p => p.UserName == model.UserName).FirstOrDefault();
                    //TheUser.IsActive = 1;
                    //_applicationContext.Update(TheUser);
                    //_applicationContext.SaveChanges();

                    ViewBag.Message = _localizer["AccountCreated"].Value;
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

                        ModelState.TryAddModelError("", _localizer["AccountInActiveMessage"].Value);
                    }

                }
                else {

                    ModelState.TryAddModelError("", _localizer["LoginFailedMessage"].Value);
                }
            }

            return View();
        }

       
        public async Task<IActionResult> Logout() {

            if (User.Identity.IsAuthenticated) {
               await _signInManager.SignOutAsync();

                HttpContext.Session.Remove("mySchoolId");
                HttpContext.Session.Remove("new");
                HttpContext.Session.Remove("SchoolID");

            }
            return RedirectToAction("Login","Account");
        }


        [AcceptVerbs("Get", "Post")]
        [Route("IsEmailUnique")]
        public async Task<IActionResult> IsEmailUnique(string email)
        {

            var founder = _applicationContext.Users.Where(p => p.Email == email).FirstOrDefault();
            if (founder == null)
            {

                return Json(true);
            }
            else
            {
                return Json(_localizer["EmailUniqueMessage"].Value);
            }

        }


        public async Task<IActionResult> ChangePasswordofOtherUsers(string UserEmail)
        {
            ViewBag.UserEmail = UserEmail;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePasswordofOtherUsers(ChangeOtherUsersPasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(changePasswordViewModel.UserEmail);


                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, changePasswordViewModel.NewPassword);
                    if (result.Succeeded)
                    {
                        ViewBag.Message = _localizer["PasswordRecoveredSuccessfully"];
                        return View("RegisterStaff");

                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(changePasswordViewModel);
                }
              

            }
            return View();

        }


        [HttpPost]

        public IActionResult LanguageChange(string languageName,string returnedUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(languageName)),
                new CookieOptions { Expires=DateTimeOffset.Now.AddDays(30) });


            return LocalRedirect(returnedUrl);
        }

    }
}