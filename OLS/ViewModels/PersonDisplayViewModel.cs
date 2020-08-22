using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using OLS.CustomValidation;
using Microsoft.AspNetCore.Mvc;

namespace OLS.ViewModels
{
    public class PersonDisplayViewModel
    {
        public  Guid  PersonId          { get; set; }
 
        public string Name                  { get; set; }

        public string LastName           { get; set; }

        public string FatherName            { get; set; }

        public string GrandFatherName       { get; set; }
        public string PhonNumber            { get; set; }

        public string Email                  { get; set; }

        public string Nidnumber             { get; set; }

        public int?   Age                 { get; set; }

        public string Photo              { get; set; }

        public string GenderType             { get; set; }

        public string EducationLevel        { get; set; }

     
        public string PerProvince           { get; set; }

        public string PerDistrict            { get; set; }

        public string PerNahia           { get; set; }

        public string PreProvince           { get; set; }

        public string PreDistrict           { get; set; }

        public string PreNahia { get; set; }
    }
}
