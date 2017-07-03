using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FTMS.Models
{
    public class User
    {
        [Required(ErrorMessage ="username is required")]
        [ExcludeChar("!@.")]
        public string username { get; set; }
        [Required(ErrorMessage ="password is required")]
        public string password { get; set; }
    }
}