using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Controllers
{
	[AllowAnonymous]
	public class AboutController : Controller
	{
		private readonly IAboutService	_aboutService;
        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
        public async Task<IActionResult> Index()
		{
			var values = await _aboutService.TGetByIdAsync(3);

			return  View(values);
		}
		public async  Task<PartialViewResult> SocailMediaAbout()
		{
			return  PartialView();
		}
	}
}
