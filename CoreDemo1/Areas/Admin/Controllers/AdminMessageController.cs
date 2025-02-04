using BusinessLayer.Abstract;
using DataAccessLayer.BaseRepository.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreDemo1.Areas.Admin.Controllers
{ 
    [Area("Admin")]
    public class AdminMessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly BlogProjectContext _context;
        private readonly UserManager<AppUser> _userManager;
        public AdminMessageController(IMessageService messageService, BlogProjectContext context, UserManager<AppUser> userManager)
        {
            _messageService = messageService;
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> InBox()
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _context.Writers.FirstOrDefaultAsync(x => x.AppUserId == user.Id);
            var writerId = writer.WriterID;
            var values = await _messageService.GetInboxListByWriter(writerId);
            return View(values);
        }
        public async Task<IActionResult> SendBox()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _context.Writers.FirstOrDefaultAsync(x => x.AppUserId == user.Id);
            var writerId = writer.WriterID;
            var values = await _messageService.GetSendboxListByWriter(writerId);
            return View(values);

        }
        [HttpGet]
        public async Task<IActionResult> ComposeMessage() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ComposeMessage(Message2 message,string ReceiverMail)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _context.Writers.FirstOrDefaultAsync(x => x.AppUserId == user.Id);
            var receiverUser = await _userManager.FindByEmailAsync(ReceiverMail);
            var receiverWriter = await _context.Writers.FirstOrDefaultAsync(x => x.AppUserId == receiverUser.Id);
            if (receiverWriter == null)
            {
                ModelState.AddModelError("", "Bu e-posta adresine sahip bir yazar bulunamadı.");
                return View(message);
            }
            var writerId = writer.WriterID;


            message.SenderID = writerId;
            message.ReceiverID = receiverWriter.WriterID;
            message.MessageDate=DateTime.UtcNow;
            message.MessageStatus = true;
            await _messageService.TAddAsync(message);
            return RedirectToAction("SendBox");
        }
    }
}
