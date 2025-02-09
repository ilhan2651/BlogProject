using BusinessLayer.Abstract;
using CoreDemo1.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]


    [Area("Admin")]
    [Route("Admin/AdminAbout")]
    public class AdminAboutController : Controller
	{
		private readonly IAboutService _aboutService;
        private const int StaticAboutId = 3;

        public AdminAboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var aboutEntity = await _aboutService.TGetByIdAsync(StaticAboutId);
            if (aboutEntity == null)
            {
                return NotFound();
            }

            var model = new AboutViewModel
            {
                AboutDetails1 = aboutEntity.AboutDetails1,
                AboutImage1 = aboutEntity.AboutImage1
            };

            return View(model);
        }

        [HttpPost]
        [Route("UpdateAbout")]
        public async Task<IActionResult> UpdateAbout(AboutViewModel model, IFormFile AboutImage1)
        {
            var aboutEntity = await _aboutService.TGetByIdAsync(StaticAboutId);
            if (aboutEntity == null)
            {
                return NotFound("Hakkımızda içeriği bulunamadı.");
            }

            if (!string.IsNullOrEmpty(model.AboutDetails1))
            {
                aboutEntity.AboutDetails1 = model.AboutDetails1;
            }
            if (AboutImage1 == null || AboutImage1.Length == 0)
            {
                model.AboutImage1 = aboutEntity.AboutImage1; 
            }
            else
            {
                var fileExtension = Path.GetExtension(AboutImage1.FileName).ToLower();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

                if (!Array.Exists(allowedExtensions, ext => ext == fileExtension))
                {
                    TempData["ErrorMessage"] = "Sadece JPG, JPEG, PNG veya GIF formatındaki dosyalar yüklenebilir!";
                    return RedirectToAction("Index");
                }

                var fileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await AboutImage1.CopyToAsync(stream);
                }

                if (!string.IsNullOrEmpty(aboutEntity.AboutImage1))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", aboutEntity.AboutImage1.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                aboutEntity.AboutImage1 = "/images/" + fileName;
            }

            await _aboutService.TUpdateAsync(aboutEntity);

            TempData["SuccessMessage"] = "Hakkımızda bilgileri başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

    }
}
