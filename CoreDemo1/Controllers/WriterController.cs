using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using CoreDemo1.Models;
using DataAccessLayer.BaseRepository.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreDemo1.Controllers
{

    public class WriterController : Controller
    {
        private readonly IWriterService _writerService;
        private readonly BlogProjectContext _context;
        private readonly UserManager<AppUser> _userManager;
        public WriterController(IWriterService writerService ,BlogProjectContext context,UserManager<AppUser> userManager)
        {
            _writerService = writerService;
            _context = context;
            _userManager = userManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            var userName = User.Identity.Name;
            ViewBag.v1= userName;
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
           
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                var writer = await _context.Writers.FirstOrDefaultAsync(w => w.AppUserId == user.Id);
                var writerId = writer.WriterID;
                var writerValues = await _writerService.TGetByIdAsync(writerId);
                return View(writerValues);
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> WriterEditProfile(Writer writer)
        {
            var validator = new WriterUpdateValidator();
            var results = validator.Validate(writer);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(writer);
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View(writer);
            }          

            var existingWriter = await _context.Writers.FirstOrDefaultAsync(w => w.AppUserId == user.Id);
            if (existingWriter == null)
            {
                ModelState.AddModelError("", "Yazar bulunamadı.");
                return View(writer);

            }
            if (string.IsNullOrEmpty(writer.WriterPassword))
            {
                writer.WriterPassword = existingWriter.WriterPassword;
            }
            if (!string.IsNullOrEmpty(writer.WriterPassword))
            {
                var removePasswordResult = await _userManager.RemovePasswordAsync(user);
                var addPasswordResult = await _userManager.AddPasswordAsync(user, writer.WriterPassword);
            }
                     
            if (string.IsNullOrEmpty(writer.ConfirmPassword))
            {
                writer.ConfirmPassword = existingWriter.ConfirmPassword;
            }
            var passwordHasher = new PasswordHasher<Writer>();
            existingWriter.WriterName = writer.WriterName;
            existingWriter.WriterMail = writer.WriterMail;
            existingWriter.WriterPassword = passwordHasher.HashPassword(existingWriter, writer.WriterPassword);
            existingWriter.WriterAbout = writer.WriterAbout;
            existingWriter.WriterImage = writer.WriterImage;
            existingWriter.ConfirmPassword= passwordHasher.HashPassword(existingWriter, writer.ConfirmPassword);

            _context.Entry(existingWriter).Property(x => x.AppUserId).IsModified = false;

            await _writerService.TUpdateAsync(existingWriter);

            return RedirectToAction("Index", "Dashboard");
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> WriterAdd()
        {
            return View();

        }
        [HttpPost]
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
