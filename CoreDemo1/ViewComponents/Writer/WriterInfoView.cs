using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.BaseRepository.Concrete;
using DocumentFormat.OpenXml.Spreadsheet;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreDemo1.ViewComponents.Writer
{
    public class WriterInfoView : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly BlogProjectContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWriterService _writerService;

        public WriterInfoView(UserManager<AppUser> userManager,IWriterService writerService)
        {
            _userManager = userManager;
            _writerService= writerService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _writerService.GetWriterByUserIdAsync(user.Id);
            var writerId = writer.WriterID;

         
            if (writer == null)
            {
                return View("Default", "Bilinmeyen Yazar");
            }

            return View("Default", writer);
        }

    }
}
