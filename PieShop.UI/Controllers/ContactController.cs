using Microsoft.AspNetCore.Mvc;

namespace PieShop.UI.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
