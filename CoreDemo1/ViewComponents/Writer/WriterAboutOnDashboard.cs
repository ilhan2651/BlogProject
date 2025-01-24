using BusinessLayer.Abstract;
using DataAccessLayer.BaseRepository.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.ViewComponents.Writer
{
    public class WriterAboutOnDashboard : ViewComponent
    {
        private readonly IWriterService _writerService;
        private readonly BlogProjectContext _context;

        public WriterAboutOnDashboard(IWriterService writerService, BlogProjectContext context)
        {
            _writerService = writerService;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userMail = User.Identity.Name;
             var writerID=_context.Writers.Where(x=>x.WriterMail==userMail).Select(x=>x.WriterID).FirstOrDefault();
            var value = await _writerService.TGetByIdAsync(writerID);
            return View(value);
        }
    }
}
