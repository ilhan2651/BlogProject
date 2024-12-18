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
    public class CategoryRepository : GenericRepository<Category>,ICategoryRepository
    {
        private readonly BlogProjectContext _context;
        public CategoryRepository(BlogProjectContext context): base (context) 
        {
            _context    = context;

        }
    }
}
