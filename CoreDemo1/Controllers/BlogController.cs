using BusinessLayer.Abstract;
using CoreDemo1.Models;
using DataAccessLayer.BaseRepository.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoreDemo1.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly BlogProjectContext _context;
        public BlogController(IBlogService blogService, ICategoryService categoryService,BlogProjectContext context)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _context    = context;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var values = await _blogService.GetBlogListWithCategory();
            return View(values);
        }
        [AllowAnonymous]
        public async Task<IActionResult> BlogReadAll(int id)
        {
            ViewBag.i = id;
            var values = await _blogService.TGetFilteredListAsync(x => x.BlogID == id);
            return View(values);
        }
        public async Task<IActionResult> BlogListByWriter()
        {
            var userMail = User.Identity.Name;
            var writerID = _context.Writers.Where(x => x.WriterMail == userMail).Select(x => x.WriterID).FirstOrDefault();
            var values = await _blogService.GetListWithCategoryByWriterBm(writerID);
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> BlogAdd()
        {
            var categories = await _categoryService.TListAllAsync();

            var model = new BlogAddViewModel
            {
                Categories = categories.Select(x => new SelectListItem
                {
                    Value = x.CategoryID.ToString(),
                    Text = x.CategoryName
                })
            };

            return View(model);
        }
        [HttpPost]

        public async Task<IActionResult> BlogAdd(BlogAddViewModel model)
        {
            var userMail = User.Identity.Name;
            var writerID = _context.Writers.Where(x => x.WriterMail == userMail).Select(x => x.WriterID).FirstOrDefault();
            ModelState.Remove("Categories");
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.TListAllAsync();
                model.Categories = categories.Select(x => new SelectListItem
                {
                    Value = x.CategoryID.ToString(),
                    Text = x.CategoryName
                });

                return View(model);
            }

            // Veritabanına kayıt için Blog nesnesi oluşturuluyor
            var blog = new Blog
            {
                BlogTitle = model.BlogTitle,
                BlogImage = model.BlogImage,
                BlogThumbnailImage = model.BlogThumbnailImage,
                BlogContent = model.BlogContent,
                CategoryID = model.CategoryID,
                BlogStatus = true,
                BlogCreateDate = DateTime.UtcNow,
                WriterID = writerID
            }; 

            await _blogService.TAddAsync(blog);

            return RedirectToAction("BlogListByWriter", "Blog");
        }
        public async Task<IActionResult> DeleteBlog(int id)
        {
            await _blogService.TDeleteAsync(id);
            return RedirectToAction("BloglistByWriter");
        }
        [HttpGet]
        public async Task<IActionResult> EditBlog(int id)
        {
            var blogValue =await _blogService.TGetByIdAsync(id);
            var categories = await _categoryService.TListAllAsync();

            var model = new BlogUpdateViewModel
            {
                BlogID = blogValue.BlogID,
                BlogTitle = blogValue.BlogTitle,
                BlogImage = blogValue.BlogImage,
                BlogThumbnailImage = blogValue.BlogThumbnailImage,
                BlogContent = blogValue.BlogContent,
                CategoryID = blogValue.CategoryID,
                Categories = categories.Select(x => new SelectListItem
                {
                    Value = x.CategoryID.ToString(),
                    Text = x.CategoryName
                })
            };
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> EditBlog(Blog p)
        {
            var userMail = User.Identity.Name;
            var writerID = _context.Writers.Where(x => x.WriterMail == userMail).Select(x => x.WriterID).FirstOrDefault();
            p.WriterID = writerID;
            p.BlogCreateDate = DateTime.UtcNow;
            p.BlogStatus = true;
            await _blogService.TUpdateAsync(p);
            return RedirectToAction("BlogListByWriter");

        }
       
    }

}
