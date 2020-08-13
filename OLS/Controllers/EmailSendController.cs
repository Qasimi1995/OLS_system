using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.Controllers
{
    public class EmailSendController:Controller
    {
        private readonly IEmailSender _emailSender;

        public EmailSendController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SendTestEmail()
        {
            var message = new Message(new string[] { "OLS Team " }, "Password Reset", " Hey Dear Please use the following link to reset your password, if you did not request this password reset please ignore the mail ");
            _emailSender.SendEmail(message); 

            return View();
        }
    }
}
