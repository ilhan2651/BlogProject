using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Extensions;
using X.PagedList.Mvc.Core;
namespace CoreDemo1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var values = await _categoryService.TListAllAsync();
            var pagedValues = values.AsQueryable().ToPagedList(page, 3);

            return View(pagedValues);
        }
        [HttpGet]
        public async Task<IActionResult> AddCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            CategoryValidator validator = new CategoryValidator();
            var results = validator.Validate(category);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }
                return View(category);
            }
            category.CategoryStatus = true;
            await _categoryService.TAddAsync(category);
            return RedirectToAction("Index", "Category");

        }
        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _categoryService.TGetByIdAsync(id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(Category category)
        {

            var existingCategory = await _categoryService.TGetByIdAsync(category.CategoryID);
            CategoryValidator validator = new CategoryValidator();
            var results = validator.Validate(category);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(category);
            }
            existingCategory.CategoryName = category.CategoryName;
            existingCategory.CategoryDescription = category.CategoryDescription;
            await _categoryService.TUpdateAsync(existingCategory);
            return RedirectToAction("Index", "Category");
        }
        public async Task<IActionResult> SetPassive(int id)
        {

            await _categoryService.SetCategoryStatus(id, false);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> SetActive(int id)
        {

            await _categoryService.SetCategoryStatus(id, true);
            return RedirectToAction("Index");
        }

    }
}
