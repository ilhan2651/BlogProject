using BusinessLayer.Concrete;
using DataAccessLayer.BaseRepository.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CoreDemo1.Controllers
{  
    public class AdminController : Controller
    {
      private readonly UserManager<AppUser> _userManager;
        private readonly BlogProjectContext _context;
        public AdminController(BlogProjectContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<PartialViewResult> AdminNavbarPartial()
        {

            return PartialView();
        }


    }
}
 