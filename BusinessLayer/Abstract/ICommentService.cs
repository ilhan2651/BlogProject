using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICommentService : IGenericService<Comment>
    {
		Task<List<Comment>> GetListByBlogId(int id);
        Task<List<Comment>> GetCommenWithBlog();
        Task<double> GetAverageScoreByBlogIdAsync(int blogId);


    }
}
