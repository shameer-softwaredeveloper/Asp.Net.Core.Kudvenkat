using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Utilities
{
    public class ValidEmailDomainAttributes : ValidationAttribute
    {
        private readonly string allowedDomain;
        public ValidEmailDomainAttributes(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }

        public override bool IsValid(object value)
        {
            string[] strings = value.ToString().Split('@');
            return strings[1].ToUpper() == allowedDomain.ToUpper();
        }
    }
}