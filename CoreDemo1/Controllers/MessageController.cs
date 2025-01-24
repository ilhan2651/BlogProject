using BusinessLayer.Abstract;
using CoreDemo1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreDemo1.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> InBox()
        {
            int id = 1;
            var values = await _messageService.GetInboxListByWriter(id);
            return View(values);

        }
        [AllowAnonymous]

        public async Task<IActionResult> MessageDetails(int id)
        {
            var value = await _messageService.GetMessageWithWriterById(id);
  
            return View(value);

        }
    }
}
