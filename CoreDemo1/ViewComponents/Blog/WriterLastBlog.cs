using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.ViewComponents.Blog
{
	public class WriterLastBlog : ViewComponent
	{
		private readonly IBlogService _blogService;
        public WriterLastBlog(IBlogService blogService)
        {
            _blogService   = blogService;
        }
		public async Task<IViewComponentResult> InvokeAsync(int writerId)
		{
			var values = await _blogService.GetLast3BlogsByWriterAsync(writerId);
			return View(values);
		}
	}
}
