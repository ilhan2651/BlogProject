using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminNotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        public AdminNotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpGet]
        public async  Task<IActionResult> Index()
        {
            var notifications = await _notificationService.TListAllAsync();
            ViewBag.NotificationCount=notifications.Count;
            return View(notifications);
        }
     
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var valueDelete = await _notificationService.TGetByIdAsync(id);
            if (valueDelete != null)
            {
                await _notificationService.TDeleteAsync(id);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult CreateNotification()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification(Notification notification)
        {

            if (!ModelState.IsValid)
            {
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("ModelState Hatası: " + modelError.ErrorMessage);
                }

                TempData["ErrorMessage"] = "Lütfen tüm alanları doldurun!";
                return View(notification);
            }

            var newNotification = new Notification
            {
                NotificationType = notification.NotificationType,
                NotificationTypeSymbol = "mdi mdi-bell",
                NotificationColor = "preview-icon bg-secondary",
                NotificationDetails = notification.NotificationDetails,
                NotificationDate = DateTime.UtcNow,  
                NotificationStatus = true 
            };
           

            await _notificationService.TAddAsync(newNotification);

            TempData["SuccessMessage"] = "Bildirim başarıyla eklendi!";
            return RedirectToAction("Index"); 
        }
    }
}
