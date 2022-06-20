using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName{get;set;}

        [Required]
        public string Password{get;set;}
    }
    public class RegisterModel
    {
        [Required]
        public string UserName{get;set;}

        [Required]
        [EmailAddress]
         [Display(Name = "Email Address")]
        public string Email{get;set;}

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password{get;set;}

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password")]
        public string ConfirmPassword{get;set;}
    }
}