using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntityLayer.Concrete;
using System.Threading.Tasks;
using DataAccessLayer.BaseRepository.Concrete;

namespace CoreDemo1.ViewComponents.Writer
{
    public class WriterInfoLayout : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly BlogProjectContext _context;

        public WriterInfoLayout(UserManager<AppUser> userManager, BlogProjectContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userName = User.Identity.Name; 

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return Content(""); 
            }

            var writer = await _context.Writers
                .Where(x => x.AppUserId == user.Id)
                .Select(x => new WriterInfoModel
                {
                    Name = x.WriterName,
                    ImageUrl = x.WriterImage
                })
                .FirstOrDefaultAsync();

            return View(writer);
        }
    }

    public class WriterInfoModel
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
