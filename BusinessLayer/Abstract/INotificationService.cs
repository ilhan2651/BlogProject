using EntityLayer.Concrete;


namespace BusinessLayer.Abstract
{
    public interface INotificationService : IGenericService<Notification>
    {
        Task<List<Notification>> GetLastFiveNotificationAsync();

    }
}
