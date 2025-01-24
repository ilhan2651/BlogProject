using BusinessLayer.Abstract;
using DataAccessLayer.Repositories.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class NotificationManager : GenericManager<Notification> ,INotificationService
    {
        private readonly  INotificationRepository _notificationRepository;
        public NotificationManager(INotificationRepository notificationRepository):base(notificationRepository) 
        {
            _notificationRepository = notificationRepository;
        }
       
    }
}
