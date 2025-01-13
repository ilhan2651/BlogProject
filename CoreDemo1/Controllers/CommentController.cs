using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Controllers
{
	public class CommentController : Controller
	{
		private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public IActionResult Index()
		{
			return View();
		}
        [HttpGet]
        public PartialViewResult PartialAddComment()
        {
            return PartialView();
        }
        [HttpPost]
        public async  Task<PartialViewResult> PartialAddComment(Comment p)
        {
            p.CommentDate = DateTime.UtcNow;
            p.BlogID = 2;
             await _commentService.TAddAsync(p);
            return PartialView();
        }
        public PartialViewResult CommentListByBlog(int id)
		{
			var values=_commentService.GetListByBlogId(id);
			return PartialView(values);
		}
	}
}
