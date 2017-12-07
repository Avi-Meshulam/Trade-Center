using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TradeCenter.Models
{
    public class Login
    {
        [Required(ErrorMessage ="Name is required")]
        [StringLength(50)]
        [Display(Name = "Name: ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50)]
        [Display(Name = "Password: ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}