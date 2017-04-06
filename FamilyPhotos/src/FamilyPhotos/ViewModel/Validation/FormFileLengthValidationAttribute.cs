using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.ViewModel.Validation
{
    public class FormFileLengthValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file==null)
            {
                return false;
            }
            return file.Length > 0;
        }
    }
}
