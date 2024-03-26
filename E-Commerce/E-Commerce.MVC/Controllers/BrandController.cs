using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.MVC.Controllers
{
    public class BrandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
