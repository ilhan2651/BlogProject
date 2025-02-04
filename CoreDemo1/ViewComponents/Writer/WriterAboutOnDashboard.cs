using BusinessLayer.Abstract;
using DataAccessLayer.BaseRepository.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreDemo1.ViewComponents.Writer
{
    public class WriterAboutOnDashboard : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWriterService _writerService;
        private readonly BlogProjectContext _context;

        public WriterAboutOnDashboard(IWriterService writerService, BlogProjectContext context,UserManager<AppUser> userManager)
        {
            _writerService = writerService;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //if (user != null)
            //{
            //    var writer = await _context.Writers.FirstOrDefaultAsync(w => w.AppUserId == user.Id);
            //    var writerId = writer.WriterID;
            //    var writerValues = await _writerService.TGetByIdAsync(writerId);
            //    return View(writerValues);
            //}
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
    }
}
