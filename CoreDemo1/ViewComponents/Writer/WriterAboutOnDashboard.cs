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

        public WriterAboutOnDashboard(IWriterService writerService, UserManager<AppUser> userManager)
        {
            _writerService = writerService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _writerService.GetWriterByUserIdAsync(user.Id);
            var writerId = writer.WriterID;
            var writerValues = await _writerService.TGetByIdAsync(writerId);
                return View(writerValues);
            
            
        }
    }
}
