using BusinessLayer.Abstract;
using DataAccessLayer.Repositories.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CommentManager : GenericManager<Comment>, ICommentService
    {
        private readonly    ICommentRepository _commentRepository;
        public CommentManager(ICommentRepository commentRepository):base(commentRepository) 
        {
             _commentRepository = commentRepository;
        }

        public async Task<double> GetAverageScoreByBlogIdAsync(int blogId)
        {
           return await _commentRepository.GetAverageScoreByBlogId(blogId);
        }

        public async Task<List<Comment>> GetCommenWithBlog()
        {
            return await   _commentRepository.GetListWithBlog();
        }

        public Task<List<Comment>> GetListByBlogId(int id)
		{
			return _commentRepository.GetFilteredList(x=>x.BlogID == id);   
		}
	}

}
