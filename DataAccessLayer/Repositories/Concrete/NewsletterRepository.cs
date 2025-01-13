using DataAccessLayer.BaseRepository.Concrete;
using DataAccessLayer.Repositories.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Concrete
{
    public class NewsletterRepository : GenericRepository<Newsletter> , INewsletterRepository
    {
        private readonly BlogProjectContext _context;
        public NewsletterRepository(BlogProjectContext context):base(context) 
        {
            _context    = context;

        }
    }
}
