using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;

namespace EmployeeManagement.Controllers
{
    public class WelcomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public WelcomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [Route("")]
        [Route("home")]
        [Route("Home/Index")]
        public ViewResult List()
        {
            var model = _employeeRepository.GetAllEmployee();
            return View("~/Views/Home/Index.cshtml", model);
        }

        [Route("Home/Details/{id?}")]
        public ViewResult Details(int? id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
              Employee = _employeeRepository.GetEmployee(id ?? 1),
              PageTitle = "Employee Details"
            };

            return View(homeDetailsViewModel); 
        }
    }
}