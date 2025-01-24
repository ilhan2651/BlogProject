using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.ViewComponents.Writer
{
    public class WriterMessageNotification : ViewComponent
    {
        private readonly IMessageService _messageService;
        public WriterMessageNotification(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            int id = 1;
            var values = await _messageService.GetInboxListByWriter(id);
            return View(values);
        }
    }
}
