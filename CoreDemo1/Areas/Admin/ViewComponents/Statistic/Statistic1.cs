using BusinessLayer.Abstract;
using DataAccessLayer.BaseRepository.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace CoreDemo1.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic1 : ViewComponent
    {
        private readonly IBlogService _blogService;
        private readonly IContactService _contactService;
        private readonly ICommentService _commentService;
        public Statistic1(IBlogService blogService, IContactService contactService,ICommentService commentService)
        {
            _blogService = blogService;
            _contactService = contactService;
            _commentService = commentService;
        } 
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blogs = await _blogService.TListAllAsync();
            ViewBag.v1 = await _blogService.GetTotalBlogsCountAsync();
            ViewBag.v2 = await _contactService.GetContactCountAsync();
            ViewBag.v3 = await _commentService.GetCommentCountAsync();


            string api = "f91caf72f8a161e4f34323a9e777759d";
            string connection = "https://api.openweathermap.org/data/2.5/weather?q=istanbul&mode=xml&lang=tr&units=metric&appid=" + api;
            XDocument document = XDocument.Load(connection);
            string tempValue = document.Descendants("temperature").ElementAt(0).Attribute("value").Value;
            string[] parts = tempValue.Split('.');
            ViewBag.v4 = parts[0];
            return View();
        }
    }
}
