using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;

namespace EmployeeManagement.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [Route("~/")]
        [Route("")]
        [Route("[action]")]
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployee();
            return View(model);
        }

        [Route("[action]/{id?}")]
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