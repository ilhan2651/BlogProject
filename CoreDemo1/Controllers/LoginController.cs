using BusinessLayer.Abstract;
using CoreDemo1.Models;
using DataAccessLayer.BaseRepository.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CoreDemo1.Controllers
{
    [AllowAnonymous]

    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly BlogProjectContext _context;
        public LoginController(BlogProjectContext context, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _context = context;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated) 
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Index(UserSignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı!");
                    return View(model); 
                }
            }
            return View(model);


        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
    }
}




//[HttpPost]

//public async Task<IActionResult> Index(Writer p)
//{
//	var datavalue = await _context.Writers.FirstOrDefaultAsync(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);
//	if (datavalue!=null)
//	{
//		var claims = new List<Claim>
//		{
//			new Claim(ClaimTypes.Name,p.WriterMail)
//		};
//		var userIdentitiy= new ClaimsIdentity(claims,"a");
//		ClaimsPrincipal principal = new ClaimsPrincipal(userIdentitiy);
//		await HttpContext.SignInAsync(principal);
//		return RedirectToAction("Index","Dashboard");
//	}
//	else
//	{
//		return View();
//	}

//}

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
