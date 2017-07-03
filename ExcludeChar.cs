using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FTMS.Models
{
    public class ExcludeChar:ValidationAttribute
    {
        private readonly string _chars;

        public ExcludeChar(string chars):base("{0} contains invalid character.")
        {
            _chars = chars;
        }

        protected override ValidationResult IsValid(object value,ValidationContext valildationcontext)
            
        {
            if (value!= null)
            {
                for(int i = 0; i < _chars.Length; i++)
                {
                    var valueString = value.ToString();
                    if (valueString.Contains(_chars[i]))
                    {
                        var errorMessage = FormatErrorMessage(valildationcontext.DisplayName);
                        return new ValidationResult(errorMessage);
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}