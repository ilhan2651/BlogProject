using BusinessLayer.Abstract;
using CoreDemo1.Areas.Admin.Models;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoreDemo1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WriterController : Controller
    {
        private readonly IWriterService _writerService;
        public WriterController(IWriterService writerService)
        {
            _writerService = writerService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> WriterList()
        {
            var authors =await _writerService.TListAllAsync();
            return Json(authors);

        }
        public async Task<IActionResult> GetWriterById(int writerId)
        {
            var findWriter =await _writerService.TGetByIdAsync(writerId);
            if (findWriter == null)
            {
                return NotFound(new {mesage= "❌ Yazar bulunamadı!" });
            }
            return Json(findWriter);

        }
        [HttpPost]
        public async Task<IActionResult> AddWriter([FromBody] Writer writer)
        {
            if (writer==null)
            {
                return BadRequest(new { message = "❌ Geçersiz veri!" });
            }
            if (writer.WriterPassword!=writer.ConfirmPassword)
            {
                return BadRequest(new { message = "❌ Şifreler uyuşmuyor!" });
            }
            writer.WriterStatus = true;
            writer.WriterAbout = "...";
            await _writerService.TAddAsync(writer);
            return Ok(new { message = "✅ Yazar başarıyla eklendi!" });
        }

        public async Task<IActionResult> DeleteWriter(int id)
        {
           var valueDelete= await _writerService.TGetByIdAsync(id);
            if (valueDelete == null)
            {
                return BadRequest(new { message = "❌ Geçersiz veri!" });

            }
            await _writerService.TDeleteAsync(id);
            return Ok(new { message = "✅ Yazar başarıyla silindi!" });
        }
        public async Task<IActionResult> UpdateWriter([FromBody] Writer writerUp)
        {
            if (writerUp == null)
            {
                return BadRequest(new { message = "❌ Geçersiz veri!" });
            }

            var writer = await _writerService.TGetByIdAsync(writerUp.WriterID);
            if (writer == null)
            {
                return NotFound(new { message = "❌ Yazar bulunamadı!" });
            }

            writer.WriterName = writerUp.WriterName;
            writer.WriterMail = writerUp.WriterMail;
            writer.WriterPassword = writerUp.WriterPassword;

            writer.WriterStatus = true;
            writer.WriterAbout = writer.WriterAbout;

            await _writerService.TUpdateAsync(writer);

            return Ok(new { message = "✅ Yazar başarıyla güncellendi!" });
        }






    }
}
