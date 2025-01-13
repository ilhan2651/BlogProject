using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Controllers
{
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
        public  PartialViewResult SubscribeMail(Newsletter p)
        {
            if (string.IsNullOrEmpty(p.Mail))
            {
                ViewBag.Message = "Email adresi gerekli!";
                return PartialView();

            }
            p.MailStatus =true;
            _newsletterService.TAddAsync(p);
            ViewBag.Message = "Abonelik başarılı";
            return  PartialView();
        }
    }
}
