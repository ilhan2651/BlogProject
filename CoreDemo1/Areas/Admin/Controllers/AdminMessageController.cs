using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CoreDemo1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]

    public class AdminMessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IWriterService _writerService;
        private readonly UserManager<AppUser> _userManager;
        public AdminMessageController(IMessageService messageService,IWriterService writerService, UserManager<AppUser> userManager)
        {
            _messageService = messageService;
            _userManager = userManager;
            _writerService = writerService;
        }
        public async Task<IActionResult> InBox(int page = 1)
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _writerService.GetWriterByUserIdAsync(user.Id);
            var writerId = writer.WriterID;
            var values = await _messageService.GetInboxListByWriter(writerId);
            ViewBag.InBoxCount = (await _messageService.GetInboxListByWriter(writerId))?.Count() ?? 0;
            ViewBag.InBoxUnreadCount = (await _messageService.GetInboxListByWriter(writerId))
                        ?.Count(m => !m.IsRead) ?? 0;
            var pagedValues = values.AsQueryable().ToPagedList(page, 8);

            return View(pagedValues);
        }
        public async Task<IActionResult> SendBox(int page=1)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _writerService.GetWriterByUserIdAsync(user.Id);
            var writerId = writer.WriterID;
            var values = await _messageService.GetSendboxListByWriter(writerId);
            ViewBag.InBoxUnreadCount = (await _messageService.GetInboxListByWriter(writerId))
                                  ?.Count(m => !m.IsRead) ?? 0;
            ViewBag.SendBoxCount=(await  _messageService.GetSendboxListByWriter(writerId)).Count();
            var pagedValues=values.AsQueryable().ToPagedList(page,8);
            return View(pagedValues);
        }
        public async Task<IActionResult> MessageDetail(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _writerService.GetWriterByUserIdAsync(user.Id);
            var writerId = writer.WriterID;
            ViewBag.InBoxUnreadCount = (await _messageService.GetInboxListByWriter(writerId))
                               ?.Count(m => !m.IsRead) ?? 0;
            var message= await _messageService.GetMessageWithWriterById(id);
            if (!message.IsRead)
            {
                message.IsRead = true;
                await _messageService.TUpdateAsync(message);
            }
            return View(message);
        }
        public async Task<IActionResult> MessageDetailSendbox(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _writerService.GetWriterByUserIdAsync(user.Id);
            var writerId = writer.WriterID;
            ViewBag.InBoxUnreadCount = (await _messageService.GetInboxListByWriter(writerId))
                               ?.Count(m => !m.IsRead) ?? 0;
            var message=await _messageService.GetMessageWithWriterReceiverUserById(id);
            return View(message);
        }




        [HttpGet]
        public async Task<IActionResult> ComposeMessage() 
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _writerService.GetWriterByUserIdAsync(user.Id);
            var writerId = writer.WriterID;
            ViewBag.InBoxUnreadCount = (await _messageService.GetInboxListByWriter(writerId))
                              ?.Count(m => !m.IsRead) ?? 0;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ComposeMessage(Message2 message,string ReceiverMail)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _writerService.GetWriterByUserIdAsync(user.Id);
            var receiverUser = await _userManager.FindByEmailAsync(ReceiverMail);
            var receiverWriter = await _writerService.GetWriterByUserIdAsync(receiverUser.Id);
            
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
