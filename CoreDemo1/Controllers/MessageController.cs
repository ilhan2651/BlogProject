using BusinessLayer.Abstract;
using CoreDemo1.Models;
using DataAccessLayer.BaseRepository.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoreDemo1.Controllers
{
    [Authorize(Roles = "Writer")]

    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
		private readonly UserManager<AppUser> _userManager;
        IWriterService _writerService;
		public MessageController(IMessageService messageService, UserManager<AppUser> userManager , IWriterService writerService)
        {
            _messageService = messageService;
            _userManager = userManager;
            _writerService = writerService;
        }
        public async Task<IActionResult> InBox()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _writerService.GetWriterByUserIdAsync(user.Id);
            var writerId = writer.WriterID;
            var values = await _messageService.GetInboxListByWriter(writerId);
            return View(values);

        }
        public async Task<IActionResult> SendBox()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _writerService.GetWriterByUserIdAsync(user.Id);
            var writerId = writer.WriterID;
            var values=await _messageService.GetSendboxListByWriter(writerId);
            return View(values);

        }



        public async Task<IActionResult> MessageDetails(int id)
        {
            var value = await _messageService.GetMessageWithWriterById(id);
  
            return View(value);

        }
        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> SendMessage(Message2 message, string ReceiverMail)
		{
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _writerService.GetWriterByUserIdAsync(user.Id);
            var writerId = writer.WriterID;


            var receiverUser = await _userManager.FindByEmailAsync(ReceiverMail);
            if (receiverUser == null)
            {
                ModelState.AddModelError("", "Bu e-posta adresine sahip bir kullanıcı bulunamadı.");
                return View(message);
            }
            var receiverWriter = await _writerService.GetWriterByUserIdAsync(receiverUser.Id);
            if (receiverWriter == null)
            {
                ModelState.AddModelError("", "Bu e-posta adresine sahip bir yazar bulunamadı.");
                return View(message);
            }
            message.SenderID= writerId;
            message.ReceiverID = receiverWriter.WriterID;
            message.MessageStatus = true;
            message.MessageDate = DateTime.UtcNow;
          await _messageService.TAddAsync(message);
			return RedirectToAction("Inbox");
		}
	}
}
