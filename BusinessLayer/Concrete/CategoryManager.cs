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
    public class CategoryManager : GenericManager<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryManager(ICategoryRepository categoryRepository) : base(categoryRepository) 
        {
           _categoryRepository  = categoryRepository;   
        }

        public async Task SetCategoryStatus(int id, bool status)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
            {
                throw new Exception("Category bulunamadı");
            }
            category.CategoryStatus = status;
            await _categoryRepository.Update(category);

        }
        

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategories();
        }

        public async Task<int> GetCategoriesCountAsync()
        {
        return await   _categoryRepository.GetCategoriesCount();
        }
    }
}
