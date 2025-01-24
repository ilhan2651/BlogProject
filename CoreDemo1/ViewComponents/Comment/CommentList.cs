using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.ViewComponents.Comment
{
    public class CommentList : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View();
        }
    }
}

