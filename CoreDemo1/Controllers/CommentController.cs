using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
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
		
        [HttpGet]
        public PartialViewResult PartialAddComment()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<JsonResult> PartialAddComment(Comment p)
        {
            CommentValidator validator= new CommentValidator();
            var result=validator.Validate(p);
            if (!result.IsValid)
            {
                return Json(result.Errors.Select(e => e.ErrorMessage).ToList());
            }

            p.CommentDate = DateTime.UtcNow;
            await _commentService.TAddAsync(p);

            return Json("Yorum başarıyla eklendi!");
            
        }
    }
}
