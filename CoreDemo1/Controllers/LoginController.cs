using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
