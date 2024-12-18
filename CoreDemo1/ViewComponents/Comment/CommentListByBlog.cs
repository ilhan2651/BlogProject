using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.ViewComponents.Comment
{
	public class CommentListByBlog : ViewComponent
	{
		private readonly ICommentService _commentService;
        public CommentListByBlog(ICommentService commentService)
        {	
		 _commentService= commentService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
		{
			var values = await _commentService.GetListByBlogId(id);
			return View(values);
		}
	}
}
