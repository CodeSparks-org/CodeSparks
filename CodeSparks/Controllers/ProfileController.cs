using Microsoft.AspNetCore.Mvc;

namespace CodeSparks.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
