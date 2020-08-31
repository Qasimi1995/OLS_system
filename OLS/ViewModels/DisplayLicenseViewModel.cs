using Microsoft.AspNetCore.Http;
using OLS.CustomValidation;
using OLS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.ViewModels
{
    public class DisplayLicenseViewModel
    {
        public School school { get; set; }
        public Person person { get; set; }

     
    }
}
