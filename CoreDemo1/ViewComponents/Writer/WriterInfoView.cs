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

        public WriterInfoView(UserManager<AppUser> userManager, BlogProjectContext context, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;

            if (string.IsNullOrEmpty(userName))
            {
                return View("Default", "Misafir"); // Kullanıcı giriş yapmamışsa
            }

            var user = await _userManager.FindByNameAsync(userName); // async bekletiyoruz

            if (user == null)
            {
                return View("Default", "Misafir");
            }

            var writer = await _context.Writers.FirstOrDefaultAsync(x => x.AppUserId == user.Id); // async bekletiyoruz

            if (writer == null)
            {
                return View("Default", "Bilinmeyen Yazar");
            }

            return View("Default", writer); // View'e writer adı gönderiliyor
        }

    }
}
