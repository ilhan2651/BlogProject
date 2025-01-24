using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.ViewComponents.Blog
{
    public class BlogListDashboard : ViewComponent
    {

        private readonly IBlogService _blogService;
        public BlogListDashboard(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _blogService.GetBlogListWithCategory();
            return View(values);
        }
    }
}
