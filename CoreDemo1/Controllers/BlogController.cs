using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using CoreDemo1.Models;
using DataAccessLayer.BaseRepository.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace CoreDemo1.Controllers
{
    public class BlogController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly BlogProjectContext _context;
        private readonly UserManager<AppUser> _userManager;
        public BlogController(IBlogService blogService, ICategoryService categoryService,BlogProjectContext context,UserManager<AppUser> userManager,ICommentService commentService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _context    = context;
            _userManager = userManager; 
            _commentService = commentService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var values = await _blogService.GetListWithCategoryAndCommentsAsync();
            return View(values);
        }
        [AllowAnonymous]
        public async Task<IActionResult> BlogReadAll(int id)
        {
            ViewBag.i = id;
            double? averageScore = await _commentService.GetAverageScoreByBlogIdAsync(id);
            ViewBag.AverageScore = averageScore.HasValue && averageScore > 0
        ? averageScore.Value.ToString("0.0") + " / 10" 
        : "Puanlanmamış";
            var blog = await _blogService.GetBlogWithCategoryAndCommentsByIdAsync(id);
            ViewBag.WriterId = blog.WriterID; 


            return View(blog);
        }
        public async Task<IActionResult> BlogListByWriter()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _context.Writers.FirstOrDefaultAsync(x=>x.AppUserId==user.Id);
           var writerId=writer.WriterID;
            var values = await _blogService.GetListWithCategoryByWriterBm(writerId);
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
            var user =await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _context.Writers.FirstOrDefaultAsync(x => x.AppUserId == user.Id);
            var writerId = writer.WriterID;

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
                WriterID = writerId
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
