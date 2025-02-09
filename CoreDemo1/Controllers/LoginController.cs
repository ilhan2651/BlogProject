using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
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
        private readonly UserManager<AppUser> _userManager;
        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true);
                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);

                        if (roles.Contains("Admin") || roles.Contains("Moderator"))
                        {
                            return RedirectToAction("Index", "Widget", new { area = "Admin" });
                        }
                        else if (roles.Contains("Writer"))
                        {
                            return RedirectToAction("Index","Dashboard");
                        }
                        
                        else
                        {
                            return RedirectToAction("Index", "Blog");
                        }
                    }
                }

                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı!");
            }
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
        public async Task<IActionResult> AccessDenided()
        {
            return View();
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
