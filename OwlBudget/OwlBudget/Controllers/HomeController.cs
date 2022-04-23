using Microsoft.AspNetCore.Mvc;

namespace OwlBudget.Controllers;

[Route("")]
[Route("login")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        // ReSharper disable once Mvc.ViewNotResolved
        return View("wwwroot/Index.cshtml");
    }
}