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
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userName= User.Identity.Name;
            ViewBag.v=userName;
            var usermail= await _context.Users.Where(x=> x.UserName == userName).Select(y=>y.Email).FirstOrDefaultAsync();
             var writerID=_context.Writers.Where(x=>x.WriterMail== usermail).Select(x=>x.WriterID).FirstOrDefault();
            var value = await _writerService.TGetByIdAsync(writerID);
            return View(value);
        }
    }
}
