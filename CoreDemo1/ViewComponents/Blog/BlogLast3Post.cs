using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.ViewComponents.Blog
{
	public class BlogLast3Post: ViewComponent
	{
		private readonly IBlogService _blogService;
		public BlogLast3Post(IBlogService blogService)
		{
			_blogService = blogService;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var values = await _blogService.GetLastThreeBlog();
			return View(values);
		}
	}
}
