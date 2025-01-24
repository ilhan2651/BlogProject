using DataAccessLayer.BaseRepository.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Controllers
{
    public class DashboardController : Controller
    {
        private readonly BlogProjectContext _context;
        public DashboardController(BlogProjectContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            ViewBag.v1=_context.Blogs.Count().ToString();
            ViewBag.v2=_context.Blogs.Where(x=>x.WriterID==1).Count();
            ViewBag.v3=_context.Categories.Count();
            return View();
        }
    }
}
