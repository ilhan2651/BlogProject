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
    public class CategoryRepository : GenericRepository<Category>,ICategoryRepository
    {
        private readonly BlogProjectContext _context;
        public CategoryRepository(BlogProjectContext context): base (context) 
        {
            _context    = context;

        }
        public void Attach(Category category) 
        {
            _context.Categories.Attach(category);
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories
                .Include(b => b.Blogs).ToListAsync();
        }
       
    }
}
