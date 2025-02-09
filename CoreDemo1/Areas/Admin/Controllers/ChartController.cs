using BusinessLayer.Abstract;
using CoreDemo1.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    [Area("Admin")]
    [Route("Admin/Chart")]
    public class ChartController : Controller
    {
        private readonly ICategoryService _categoryService;
        public ChartController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("CategoryChart")]
        public async Task<IActionResult> CategoryChart()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var model = categories.Select(c => new CategoryIdNameViewModel
            {
                categoryCount = c.Blogs.Count(),
                categoryName = c.CategoryName
            }).ToList();

            return Json(model);
        }
    }
}
