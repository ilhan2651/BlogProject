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
        private readonly IWriterService _writerService;

            
        public WriterMessageNotification(IMessageService messageService,UserManager<AppUser> userManager,IWriterService writerService)
        {
            _messageService = messageService;
            _userManager = userManager;
            _writerService = writerService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _writerService.GetWriterByUserIdAsync(user.Id);
           var writerId=writer.WriterID;
            var values = await _messageService.GetInboxListByWriterOrderingDate(writerId);
            ViewBag.MessageCount= await _messageService.GetTotalMessageCount(writerId);
            return View(values);
        }
    }
}
