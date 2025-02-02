using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Controllers
{
    [AllowAnonymous]
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
        public async Task<JsonResult> PartialAddComment(Comment p)
        {
            if (string.IsNullOrEmpty(p.CommentUserName) || string.IsNullOrEmpty(p.CommentContent))
            {
                return Json("Ad ve Yorum alanı zorunludur!");
            }

            p.CommentDate = DateTime.UtcNow;
            await _commentService.TAddAsync(p);

            return Json("Yorum başarıyla eklendi!");
        }
    }
}
