using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using CoreDemo1.Models;
using DataAccessLayer.BaseRepository.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreDemo1.Controllers
{

    public class WriterController : Controller
    {
        private readonly IWriterService _writerService;
        private readonly BlogProjectContext _context;
        public WriterController(IWriterService writerService ,BlogProjectContext context)
        {
            _writerService = writerService;
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            var userMail = User.Identity.Name;
            ViewBag.v1=userMail;
            return View();
        }
        public IActionResult WriterProfile()
        {
            return View();
        }

        public IActionResult WriterMail()
        {

            return View();
        }
        [AllowAnonymous]
        public IActionResult Test()
        {
            return View();
        }

        [AllowAnonymous]
        public PartialViewResult WriterNavbarPartial()
        {
            return PartialView();
        }
        [AllowAnonymous]

        public PartialViewResult WriterFooterPArtial()
        {
            return PartialView();
        }
        [HttpGet]
        public async Task<IActionResult> WriterEditProfile()
        {
            var userMail = User.Identity.Name;
            var writerID = _context.Writers.Where(x => x.WriterMail == userMail).Select(x => x.WriterID).FirstOrDefault();
            var writerValues = await _writerService.TGetByIdAsync(writerID);
            return View(writerValues);
        }
        [HttpPost]
        public async Task<IActionResult> WriterEditProfile(Writer writer)
        {
            var validator = new WriterValidator();
            var results = validator.Validate(writer);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(writer);
            }
            await _writerService.TUpdateAsync(writer);
            return RedirectToAction("Index","Dashboard");
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> WriterAdd()
        {
            return View();

        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> WriterAdd(AddProfileImage api)
        {
            Writer w = new Writer();
            if (api.WriterImage != null)
            {
                var extension = Path.GetExtension(api.WriterImage.FileName);
                var newImage=Guid.NewGuid()+extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/",newImage );
                var stream = new FileStream(location,FileMode.Create);
                api.WriterImage.CopyTo(stream);
                w.WriterImage = newImage;
            }
            w.WriterMail= api.WriterMail;
            w.WriterName= api.WriterName;
            w.WriterPassword= api.WriterPassword;
            w.ConfirmPassword= api.ConfirmPassword;
            w.WriterStatus= api.WriterStatus;
            w.WriterAbout= api.WriterAbout;
            await _writerService.TAddAsync(w);
            return RedirectToAction("Index", "Dashboard");

        }
    }
}
