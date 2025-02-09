using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using CoreDemo1.Areas.Admin.Models;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using DocumentFormat.OpenXml.Wordprocessing;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoreDemo1.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]

    [Area("Admin")]
    public class WriterController : Controller
    {
        private readonly IWriterService _writerService;
        private readonly UserManager<AppUser> _userManager;
        public WriterController(IWriterService writerService, UserManager<AppUser> userManager)
        {
            _writerService = writerService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> WriterList()
        {
            var authors = await _writerService.TListAllAsync();
            return Json(authors);

        }
        public async Task<IActionResult> GetWriterById(int writerId)
        {
            var findWriter = await _writerService.TGetByIdAsync(writerId);
            if (findWriter == null)
            {
                return NotFound(new { mesage = "❌ Yazar bulunamadı!" });
            }
            return Json(findWriter);

        }
        [HttpPost]
        public async Task<IActionResult> AddWriter([FromBody] Writer writer)
        {
            if (writer == null)
            {
                return BadRequest(new { message = "❌ Geçersiz veri!" });
            }
            if (writer.WriterPassword != writer.ConfirmPassword)
            {
                return BadRequest(new { message = "❌ Şifreler uyuşmuyor!" });
            }
            var user = new AppUser
            {
                UserName = writer.WriterUserName,
                Email = writer.WriterMail,
                NameSurname = writer.WriterName,
                ImageUrl = "/writer/assets/images/faces/face4.jpg"

            };
            var createUserResult = await _userManager.CreateAsync(user, writer.WriterPassword);
            if (!createUserResult.Succeeded)
            {
                return BadRequest(new { message = "❌ Kullanıcı eklenirken hata oluştu!", errors = createUserResult.Errors });
            }

            writer.WriterStatus = true;
            writer.WriterAbout = "Yeni Yazar Profili";
            writer.AppUserId = user.Id;
            writer.WriterImage = "/writer/assets/images/faces/face4.jpg";
            writer.WriterPassword = user.PasswordHash;
            writer.ConfirmPassword = user.PasswordHash;
            await _writerService.TAddAsync(writer);
            return Ok(new { message = "✅ Yazar başarıyla eklendi!" });
        }

        public async Task<IActionResult> DeleteWriter(int id)
        {
            var valueDelete = await _writerService.TGetByIdAsync(id);
            if (valueDelete == null)
            {
                return BadRequest(new { message = "❌ Geçersiz veri!" });

            }
            await _writerService.TDeleteAsync(id);
            return Ok(new { message = "✅ Yazar başarıyla silindi!" });
        }
        public async Task<IActionResult> UpdateWriter([FromBody] Writer writer)
        {
            var validator = new WriterUpdateValidator();
            var results = validator.Validate(writer);
            if (!results.IsValid)
            {    
                    return BadRequest(new { message = "❌ Geçersiz veri!", errors = results.Errors });           
            }
            var existingWriter = await _writerService.TGetByIdAsync(writer.WriterID);
            if (existingWriter == null)
            {
                return NotFound(new { message = "❌ Güncellenecek yazar bulunamadı!" });

            }
            var user = await _userManager.FindByIdAsync(existingWriter.AppUserId.ToString());
            if (user == null)
            {
                return NotFound(new { message = "❌ Güncellenecek yazarın kullanıcısı bulunamadı!" });

            }
            if (!string.IsNullOrWhiteSpace(writer.WriterPassword))
            {
                var removePasswordResult = await _userManager.RemovePasswordAsync(user);
                if (!removePasswordResult.Succeeded)
                {
                    return BadRequest(new { message = "❌ Şifre kaldırma hatası!", errors = removePasswordResult.Errors });
                }
                var addPasswordResult = await _userManager.AddPasswordAsync(user, writer.WriterPassword);
                if (!addPasswordResult.Succeeded)
                {
                    return BadRequest(new { message = "❌ Şifre ekleme hatası!", errors = addPasswordResult.Errors });

                }
                existingWriter.WriterPassword = user.PasswordHash;
                existingWriter.ConfirmPassword = user.PasswordHash;
            }
            existingWriter.WriterName = writer.WriterName;
            existingWriter.WriterMail = writer.WriterMail;
            existingWriter.WriterUserName = writer.WriterUserName;
            existingWriter.WriterAbout = existingWriter.WriterAbout;
            await _writerService.TUpdateAsync(existingWriter);

            user.Email = writer.WriterMail;
            user.NameSurname = writer.WriterName;
            user.UserName = writer.WriterUserName;
            
            var updateUserResult = await _userManager.UpdateAsync(user);
            if (!updateUserResult.Succeeded)
            {
                return BadRequest(new { message = "❌ Kullanıcı bilgileri güncellenirken hata oluştu!", errors = updateUserResult.Errors });

            }

            return Ok(new { message = "✅ Güncelleme başarılı!" });

        }
    }
}