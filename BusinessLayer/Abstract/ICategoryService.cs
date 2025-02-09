using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task SetCategoryStatus(int id, bool status);
        Task<List<Category>> GetAllCategoriesAsync();
         Task<int> GetCategoriesCountAsync();


    }
}
