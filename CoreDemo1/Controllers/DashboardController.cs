using BusinessLayer.Concrete;
using DataAccessLayer.BaseRepository.Concrete;
using DocumentFormat.OpenXml.Spreadsheet;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class DashboardController : Controller
{
    private readonly BlogProjectContext _context;
    private readonly UserManager<AppUser> _userManager;

    public DashboardController(BlogProjectContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {

        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var writer = await _context.Writers.FirstOrDefaultAsync(w => w.AppUserId == user.Id);
        var writerId = writer.WriterID;

        ViewBag.v1 = _context.Blogs.Count().ToString();
        ViewBag.v2 = _context.Blogs.Where(x => x.WriterID == writerId).Count();
        ViewBag.v3 = _context.Categories.Count();
        return View();
    }
}
