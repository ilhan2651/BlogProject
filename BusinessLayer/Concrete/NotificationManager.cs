using BusinessLayer.Abstract;
using DataAccessLayer.Repositories.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class NotificationManager : GenericManager<Notification> ,INotificationService
    {
        private readonly  INotificationRepository _notificationRepository;
        public NotificationManager(INotificationRepository notificationRepository):base(notificationRepository) 
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<List<Notification>> GetLastFiveNotificationAsync()
        {
            return await _notificationRepository.GetLastFiveNotification();
        }
    }
}
