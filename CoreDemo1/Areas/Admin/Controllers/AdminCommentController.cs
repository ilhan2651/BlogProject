using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]

    public class AdminCommentController : Controller
    {
        private readonly ICommentService _commentService;
        public AdminCommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public async Task<IActionResult> Index()
        {
            var values = await _commentService.GetCommenWithBlog();
            return View(values);
        }
        public async Task<IActionResult> DeleteComment(int id)
        {
            var valueDelete = await _commentService.TGetByIdAsync(id);
            if (valueDelete != null)
            {
                await _commentService.TDeleteAsync(id);
            }
            return RedirectToAction("Index");
        }
    }
}
