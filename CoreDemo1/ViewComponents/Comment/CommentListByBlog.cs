using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.ViewComponents.Comment
{
    public class CommentListByBlog : ViewComponent
    {
        private static readonly Random _rnd = new Random();

        private readonly ICommentService _commentService;
        public CommentListByBlog(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var values = await _commentService.GetListByBlogId(id);

            string[] images = new string[]
{
        "/web/images/facesAI/t1.jpg",
        "/web/images/facesAI/t2.jpg",
        "/web/images/facesAI/t3.jpg",
        "/web/images/facesAI/t4.jpg",
        "/web/images/facesAI/t5.jpg",
        "/web/images/facesAI/t6.jpg",
        "/web/images/facesAI/t7.jpg",
        "/web/images/facesAI/t8.jpg",
        "/web/images/facesAI/t9.jpg",
        "/web/images/facesAI/t10.jpg"
         };
            ViewBag.CommentImages = values.Select(_ => images[_rnd.Next(images.Length)]).ToList();


            return View(values);
        }
    }
}
