using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Abstract
{
   public interface IBlogService : IGenericService<Blog>
    {
        Task<List<Blog>> GetBlogListWithCategory();
        Task<List<Blog>> GetBlogListByCategoryId(int id);
        Task<List<Blog>> GetLastThreeBlog();

        Task<List<Blog>> GetListWithCategoryByWriterBm(int id);
        Task<List<Blog>> GetListWithCategoryAndCommentsAsync();
        Task<Blog> GetBlogWithCategoryAndCommentsByIdAsync(int id);
        Task<List<Blog>> MostCommented3PostAsync();
        Task<List<Blog>> GetLast3BlogsByWriterAsync(int id);


    }
}
