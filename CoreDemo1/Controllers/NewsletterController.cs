using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Controllers
{
    [AllowAnonymous]
    public class NewsletterController : Controller
    {
     private readonly INewsletterService _newsletterService;
        public NewsletterController(INewsletterService newsletterService)
        {
            _newsletterService  = newsletterService;
        }
        [HttpGet]   
        public  PartialViewResult SubscribeMail()
        {
            return  PartialView();
        }
        [HttpPost]
        public JsonResult SubscribeMail(Newsletter p)
        {
            if (string.IsNullOrEmpty(p.Mail))
            {
                return Json("Email adresi gerekli!");
            }
            p.MailStatus = true;
            _newsletterService.TAddAsync(p);
            return Json("Abonelik başarılı!");
        }
    }
}
