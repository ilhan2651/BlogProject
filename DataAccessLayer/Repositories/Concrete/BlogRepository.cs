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
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        private readonly BlogProjectContext _context;
        public BlogRepository(BlogProjectContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<Blog>> GetListByBlogID(int id)
        {
            return await  GetFilteredList(a=>a.CategoryID == id);
        }

        public async Task<List<Blog>> GetListWithCategory()
		{
			return await _context.Blogs.Include(b => b.Category).ToListAsync();
		}

        public async Task<List<Blog>> GetListWithCategoryByWriter(int id)
        {
            return await _context.Blogs
                .Include(b => b.Category)
                .Where(b => b.WriterID == id).ToListAsync();
        }
       
    }
}
