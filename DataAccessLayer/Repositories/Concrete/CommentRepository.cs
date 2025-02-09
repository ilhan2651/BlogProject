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
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly BlogProjectContext _context;
        public CommentRepository(BlogProjectContext context) : base(context) 
        {
            _context    = context;

        }

        public async Task<List<Comment>> GetListWithBlog()
        {
            return await _context.Comments.Include(x=>x.Blog).ToListAsync();
        }
        public async Task<double> GetAverageScoreByBlogId(int blogId)
        {
            return await _context.Comments
                .Where(c => c.BlogID == blogId) 
                .Select(c => (double?)c.BlogScore) 
                .AverageAsync() ?? 0; 
        }

        public async Task<int> GetCommentCount()
        {
            return await _context.Comments .CountAsync();
        }
    }
}
 