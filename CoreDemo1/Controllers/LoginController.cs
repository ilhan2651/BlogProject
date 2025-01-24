using BusinessLayer.Abstract;
using DataAccessLayer.BaseRepository.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CoreDemo1.Controllers
{
	public class LoginController : Controller
	{
		private readonly BlogProjectContext _context;
        public LoginController(BlogProjectContext context)
        {
            _context	= context;
        }
        [AllowAnonymous]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Index(Writer p)
		{
			var datavalue = await _context.Writers.FirstOrDefaultAsync(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);
			if (datavalue!=null)
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name,p.WriterMail)
				};
				var userIdentitiy= new ClaimsIdentity(claims,"a");
				ClaimsPrincipal principal = new ClaimsPrincipal(userIdentitiy);
				await HttpContext.SignInAsync(principal);
				return RedirectToAction("Index","Dashboard");
			}
			else
			{
				return View();
			}

		}
	}
}
//var datavalue = await _context.Writers.FirstOrDefaultAsync(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);
//if (datavalue != null)
//{
//	HttpContext.Session.SetString("username", p.WriterMail);
//	return RedirectToAction("Index", "Writer");

//}
//else
//{
//	return View();
//}
