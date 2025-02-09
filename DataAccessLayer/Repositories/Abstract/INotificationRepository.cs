using DataAccessLayer.BaseRepository.Abstract;
using EntityLayer.Concrete;


namespace DataAccessLayer.Repositories.Abstract
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        Task<List<Notification>> GetLastFiveNotification();
    }
}
