using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> rolemanager;
        public AdministrationController(RoleManager<IdentityRole> rolemanager)
        {
            this.rolemanager = rolemanager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
    }
}