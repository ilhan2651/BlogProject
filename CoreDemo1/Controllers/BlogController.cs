using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Controllers
{
    
    public class BlogController : Controller
    {
		private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<IActionResult> Index()
        {
            var values = await _blogService.GetBlogListWithCategory();
            return View(values);
        }
        public async Task<IActionResult> BlogReadAll(int id)
        {
            ViewBag.i=id;
            var values = await _blogService.TGetFilteredListAsync(x=>x.BlogID==id);
            return  View(values);
        }
    } 
}
