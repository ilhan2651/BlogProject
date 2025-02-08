using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.ViewComponents.Blog
{
	public class BlogMost3Commented : ViewComponent
	{
		private readonly IBlogService _blogService;
		public BlogMost3Commented(IBlogService blogService)
		{
			_blogService = blogService;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var values = await _blogService.MostCommented3PostAsync();
			return View(values);
		}
	}
}
