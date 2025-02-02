using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Controllers
{
	[AllowAnonymous]
	public class ContactController : Controller
	{
		private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService	= contactService;
        }
        [HttpGet]
		public  IActionResult Index()
		{
			return   View();
		}
		[HttpPost]
		public IActionResult Index(Contact p)
		{
			p.ContactDate = DateTime.UtcNow;
			p.ContactStatus = true;
			_contactService.TAddAsync(p);
			return RedirectToAction("Index","Blog");
		}
	}
}
