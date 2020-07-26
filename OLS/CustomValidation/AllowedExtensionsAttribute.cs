using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.CustomValidation
{
    public class AllowedExtensionsAttribute:ValidationAttribute
{
    private readonly string[] _Extensions;
    public AllowedExtensionsAttribute(string[] Extensions)
    {
        _Extensions = Extensions;
    }

    public override bool IsValid(object value)
    {
        var file = value as IFormFile;
        
        if (!(file == null))
        {
                var extension = Path.GetExtension(file.FileName);
                if (_Extensions.Contains(extension.ToLower()))
                    {
                            return true;
                    }
        }

            return false;
    }

  
}
}
