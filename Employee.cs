using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FTMS.Models
{
    public class Employee
    {
        [Key]
        [Required(ErrorMessage ="EmployeeId required")]
        public int empId { get; set; }

        [Required(ErrorMessage ="EmployeeName is required")]
        [StringLength(20,MinimumLength =4)]
        public string empName { get; set; }

        [StringLength(10,MinimumLength =2)]
        [Required(ErrorMessage ="Category is required")]
        public string category { get; set; }

        [StringLength(10,MinimumLength=2)]
        [Required(ErrorMessage ="Tool is required")]
        public string tool { get; set; }

        [Required(ErrorMessage ="Designation is required")]
        [StringLength(10)]
        public string designation { get; set; }

        [Required(ErrorMessage ="DateOfBirth is required")]
        [RegularExpression(@"([0][1-9]|[1][0-9|][2][0-9]|[3][0-1])\/([0][1-9]|[1][0-2])\/[1-2][0-9][0-9][0-9]")]
        public DateTime dob { get; set; }
    }
}