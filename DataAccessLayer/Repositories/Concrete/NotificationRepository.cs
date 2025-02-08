using DataAccessLayer.BaseRepository.Concrete;
using DataAccessLayer.Repositories.Abstract;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Concrete
{
    public class NotificationRepository: GenericRepository<Notification>,INotificationRepository
    {
        private readonly BlogProjectContext _context;
        public NotificationRepository(BlogProjectContext context):base(context) 
        {
            _context = context;
        }
      
    }
}
