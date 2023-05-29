using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email {get; set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password {get; set;}

        // IsPersistent Cookie. Checkbox
        [Display(Name = "Remember me")]
        public bool RememberMe {get; set;}
    }
}