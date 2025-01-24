using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.ViewComponents.Writer
{
    public class WriterNotification : ViewComponent
    {
        private readonly INotificationService _notificationService;
        public WriterNotification(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notifications = await _notificationService.TListAllAsync();
         var filteredNotifications= notifications.Where(n => n.NotificationStatus == true)
          .OrderByDescending(n => n.NotificationID)
          .Take(5)
          .ToList();
            return View(notifications);

        }
    }
}
