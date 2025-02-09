using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Writer")]

        public async  Task<IActionResult> Index()
        {
            var values =await _categoryService.TListAllAsync();
            return View(values);
        }
    }
}
