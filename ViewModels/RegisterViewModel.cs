using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Utilities;

namespace EmployeeManagement.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(controller:"Account", action:"IsEmailInUse")]
        [ValidEmailDomainAttributes(allowedDomain:"gmail.com", ErrorMessage = "Email domain must be gmail.com")]
        public string Email {get; set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password {get; set;}

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password donot match")]
        public string ConfirmPassword {get; set;}
    }
}