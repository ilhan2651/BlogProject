using BusinessLayer.Abstract;
using DataAccessLayer.BaseRepository.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreDemo1.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic2 : ViewComponent
    {  
        private readonly IContactService _contactService;
        private readonly ICommentService _commentService;
        private readonly IBlogService _blogService;
        public Statistic2(BlogProjectContext context, IContactService contactService, ICommentService commentService, IBlogService blogService)
        {
            _contactService = contactService;
            _commentService = commentService;
            _blogService = blogService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.v1 = await _blogService.GetLastBlogAsync();
            ViewBag.v2 = await _contactService.GetContactCountAsync();
            ViewBag.v3 = await _commentService.GetCommentCountAsync();
            return View();
        }
    }
}
