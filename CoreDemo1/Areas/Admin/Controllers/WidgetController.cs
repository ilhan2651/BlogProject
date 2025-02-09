using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]

    [Area("Admin")]
    public class WidgetController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
