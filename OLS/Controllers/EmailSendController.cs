using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.Controllers
{
    public class EmailSendController:Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly IHtmlLocalizer _localizer;
        public EmailSendController(IEmailSender emailSender, IHtmlLocalizer<EmailSendController> localizer)
        {
            _emailSender = emailSender;
            _localizer = localizer;

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SendTestEmail()
        {
            var message = new Message(new string[] { _localizer["OLSTeam"].Value }, _localizer["PasswordReset"].Value, _localizer["Message"].Value);
            _emailSender.SendEmail(message); 

            return View();
        }
    }
}
