using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogue.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
