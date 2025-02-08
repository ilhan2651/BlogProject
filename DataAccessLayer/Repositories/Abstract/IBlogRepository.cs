using DataAccessLayer.BaseRepository.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Abstract
{
    public interface IBlogRepository : IGenericRepository<Blog>
    {
        Task<List<Blog>> GetListWithCategory();
        Task<List<Blog>> GetListByBlogID(int id);
        Task<List<Blog>> GetListWithCategoryByWriter(int id);
        Task<List<Blog>> GetListWithCategoryAndComments();
        Task<Blog> GetBlogWithCategoryAndCommentsById(int id);

        Task<List<Blog>> MostCommented3Post();
        Task<List<Blog>> GetLast3BlogsByWriter(int id);

    }
}
