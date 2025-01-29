using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
namespace CoreDemo1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async  Task<IActionResult> Index()
        {
            var values =await _categoryService.TListAllAsync();
            return View(values);
        }
    }
}
