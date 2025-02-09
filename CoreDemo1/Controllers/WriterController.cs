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
    [Authorize(Roles = "Writer")]

    public class WriterController : Controller
    {
        private readonly IWriterService _writerService;
        private readonly UserManager<AppUser> _userManager;
        public WriterController(IWriterService writerService ,UserManager<AppUser> userManager)
        {
            _writerService = writerService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var userName = User.Identity.Name;
            ViewBag.v1= userName;
            return View();
        }

        public PartialViewResult WriterNavbarPartial()
        {
            return PartialView();
        }

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
                var writer = await _writerService.GetWriterByUserIdAsync(user.Id);
                var writerId = writer.WriterID;
                var writerValues = await _writerService.TGetByIdAsync(writerId);
                return View(writerValues);
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> WriterEditProfile(Writer writer, IFormFile WriterImageFile)
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

            var existingWriter = await _writerService.GetWriterByUserIdAsync(user.Id);
            if (existingWriter == null)
            {
                ModelState.AddModelError("", "Yazar bulunamadı.");
                return View(writer);
            }

            if (WriterImageFile != null && WriterImageFile.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(WriterImageFile.FileName);
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await WriterImageFile.CopyToAsync(fileStream);
                }

                existingWriter.WriterImage = "/images/" + uniqueFileName;
            }

            if (!string.IsNullOrWhiteSpace(writer.WriterPassword))
            {
                var removePasswordResult = await _userManager.RemovePasswordAsync(user);
                if (!removePasswordResult.Succeeded)
                {
                    foreach (var error in removePasswordResult.Errors)
                    {
                        ModelState.AddModelError("", $"Şifre kaldırma hatası: {error.Description}");
                    }
                    return View(writer);
                }

                var addPasswordResult = await _userManager.AddPasswordAsync(user, writer.WriterPassword);
                if (!addPasswordResult.Succeeded)
                {
                    foreach (var error in addPasswordResult.Errors)
                    {
                        ModelState.AddModelError("", $"Şifre ekleme hatası: {error.Description}");
                    }
                    return View(writer);
                }

                var passwordHasher = new PasswordHasher<Writer>();
                existingWriter.WriterPassword = passwordHasher.HashPassword(existingWriter, writer.WriterPassword);
                existingWriter.ConfirmPassword = passwordHasher.HashPassword(existingWriter, writer.WriterPassword);
            }

            existingWriter.WriterName = writer.WriterName;
            existingWriter.WriterMail = writer.WriterMail;
            existingWriter.WriterAbout = writer.WriterAbout;
            existingWriter.AppUserId=existingWriter.AppUserId;
            await _writerService.TUpdateAsync(existingWriter);

            user.Email = writer.WriterMail;
            user.NameSurname = writer.WriterName;
            var updateUserResult = await _userManager.UpdateAsync(user);
            if (!updateUserResult.Succeeded)
            {
                foreach (var error in updateUserResult.Errors)
                {
                    ModelState.AddModelError("", $"Kullanıcı bilgileri güncellenirken hata oluştu: {error.Description}");
                }
                return View(writer);
            }

            return RedirectToAction("Index", "Dashboard");
        }


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
