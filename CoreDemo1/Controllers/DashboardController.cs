using DataAccessLayer.BaseRepository.Concrete;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class DashboardController : Controller
{
    private readonly BlogProjectContext _context;

    public DashboardController(BlogProjectContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {

        var userName = User.Identity.Name;
        var usermail = await _context.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefaultAsync();
        var writerId= await _context.Writers.Where(x => x.WriterMail==usermail).Select(y=>y.WriterID).FirstOrDefaultAsync();
        ViewBag.v1 = _context.Blogs.Count().ToString();
        ViewBag.v2 = _context.Blogs.Where(x => x.WriterID == writerId).Count();
        ViewBag.v3 = _context.Categories.Count();
        return View();
    }
}
