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
            return await GetFilteredList(a => a.CategoryID == id);
        }

        public async Task<List<Blog>> GetListWithCategory()
        {
            return await _context.Blogs
                .Include(b => b.Category)
                .Include(b => b.Comments)
                .ToListAsync();
        }
        public async Task<List<Blog>> GetListWithCategoryAndComments()
        {
            return await _context.Blogs
                .Include(b => b.Category)
                .Include(b => b.Comments)
                .OrderByDescending(x => x.BlogCreateDate)
                .ToListAsync();
        }

        public async Task<Blog> GetBlogWithCategoryAndCommentsById(int id)
        {

            return  await _context.Blogs
                .Include(b=>b.Writer)
                .Include(b => b.Category)
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(x=>x.BlogID==id);
        }

        public async Task<List<Blog>> GetListWithCategoryByWriter(int id)
        {
            return await _context.Blogs
                .Include(b => b.Category)
                .Where(b => b.WriterID == id).ToListAsync();
        }

		public async Task<List<Blog>> MostCommented3Post()
		{
			return await _context.Blogs
                .Include(b=>b.Comments)
                .OrderByDescending(b=>b.Comments.Count)
                .Take(3)
                .ToListAsync();
		}
        public async Task<List<Blog>> GetLast3BlogsByWriter(int id)
        {
            return await _context.Blogs
                .Where(b => b.WriterID == id)
                .OrderByDescending(b => b.BlogCreateDate) 
                .Take(3)
                .ToListAsync();
        }

        public async Task<int> GetWriterBlogCount(int id)
        {
          return await _context.Blogs.Where(x => x.WriterID == id).CountAsync();
        }

        public async Task<int> GetTotalBlogsCount()
        {
           return await _context.Blogs.CountAsync();
        }

        public async Task<string> GetLastBlog()
        {
            return await _context.Blogs
             .OrderByDescending(x => x.BlogID)
             .Select(x => x.BlogTitle)
             .FirstOrDefaultAsync();
        }

        public async Task<List<Blog>> GetLastThreeBlog()
        {
            return await  _context.Blogs
                .OrderByDescending(b => b.BlogCreateDate)
                .Take(3)
                .ToListAsync();
        }
    }
}
