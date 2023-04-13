using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement
{
    public class HomeController : Controller
    {
        // http://localhost:5000/
        // http://localhost:5000/home/index
        public JsonResult Index()
        {
            //return "Hello from MVC";
            return Json(new {id=1, name="Shameer"});
        }
    }
}