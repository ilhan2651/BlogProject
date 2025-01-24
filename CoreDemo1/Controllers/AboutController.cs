using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Controllers
{
	public class AboutController : Controller
	{
		private readonly IAboutService	_aboutService;
        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
        public async Task<IActionResult> Index()
		{
			var values = await _aboutService.TListAllAsync();

			return  View(values);
		}
		public async  Task<PartialViewResult> SocailMediaAbout()
		{
			return  PartialView();
		}
	}
}
