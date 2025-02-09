using DataAccessLayer.BaseRepository.Concrete;
using DataAccessLayer.Repositories.Abstract;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Repositories.Concrete
{
    public class NotificationRepository: GenericRepository<Notification>,INotificationRepository
    {
        private readonly BlogProjectContext _context;
        public NotificationRepository(BlogProjectContext context):base(context) 
        {
            _context = context;
        }

        public async Task<List<Notification>> GetLastFiveNotification()
        {
         return await    _context.Notifications.Where(n => n.NotificationStatus == true)
           .OrderByDescending(n => n.NotificationID)
           .Take(5)
           .ToListAsync();
        }
    }
}
