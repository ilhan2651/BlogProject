using BusinessLayer.Abstract;
using DataAccessLayer.BaseRepository.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Areas.ViewComponents.Statistic
{
    public class Statistic1 : ViewComponent
    {
        private readonly BlogProjectContext _context;
        private readonly IBlogService _blogService;
        public Statistic1(IBlogService blogService, BlogProjectContext context)
        {
            _blogService = blogService;
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
           var blogs = await _blogService.TListAllAsync();
            ViewBag.v1 = blogs.Count;
            ViewBag.v2 =  _context.Contacts.Count();
            ViewBag.v3= _context.Comments.Count();
            return View();
        }
    }
}
