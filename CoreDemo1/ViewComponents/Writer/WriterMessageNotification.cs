using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.BaseRepository.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreDemo1.ViewComponents.Writer
{
    public class WriterMessageNotification : ViewComponent
    {
        private readonly IMessageService _messageService;
        private readonly UserManager<AppUser> _userManager;
        private readonly BlogProjectContext _context;

            
        public WriterMessageNotification(IMessageService messageService,UserManager<AppUser> userManager,BlogProjectContext context)
        {
            _messageService = messageService;
            _userManager = userManager;
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _context.Writers.FirstOrDefaultAsync(x => x.AppUserId == user.Id);
            var writerId = writer.WriterID;
            var values = await _messageService.GetInboxListByWriterOrderingDate(writerId);
            ViewBag.MessageCount= await _messageService.GetTotalMessageCount(writerId);
            return View(values);
        }
    }
}
