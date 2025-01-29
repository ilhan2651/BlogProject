using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WidgetController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
