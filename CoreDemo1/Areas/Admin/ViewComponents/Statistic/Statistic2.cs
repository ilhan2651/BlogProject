using BusinessLayer.Abstract;
using DataAccessLayer.BaseRepository.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreDemo1.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic2 : ViewComponent
    {
        private readonly BlogProjectContext _context;
        public Statistic2(BlogProjectContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.v1 = await _context.Blogs.OrderByDescending(x => x.BlogID).Select(x => x.BlogTitle).Take(1).FirstOrDefaultAsync();
            ViewBag.v2 = _context.Contacts.Count();
            ViewBag.v3 = _context.Comments.Count();
            return View();
        }
    }
}
