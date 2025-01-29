using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.ViewComponents.Category
{
    public class CategoryList : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoryList(ICategoryService categoryService )
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _categoryService.GetAllCategoriesAsync();
            return View( values );
        }
    }
}
