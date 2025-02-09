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
        private readonly UserManager<AppUser> _userManager;
        private readonly IWriterService _writerService;
        public BlogController(IBlogService blogService, ICategoryService categoryService,IWriterService writerService,UserManager<AppUser> userManager,ICommentService commentService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _userManager = userManager; 
            _commentService = commentService;
            _writerService = writerService;
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
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> BlogListByWriter()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _writerService.GetWriterByUserIdAsync(user.Id);
           var writerId=writer.WriterID;
            var values = await _blogService.GetListWithCategoryByWriterBm(writerId);
            return View(values);
        }
        [Authorize(Roles = "Writer")]
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

        [Authorize(Roles = "Writer")]

        [HttpPost]
        public async Task<IActionResult> BlogAdd(BlogAddViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _writerService.GetWriterByUserIdAsync(user.Id);
            var writerId = writer.WriterID;
            ModelState.Remove("BlogImage");
            ModelState.Remove("BlogThumbnailImage");
            ModelState.Remove("Categories"); 

            if (!ModelState.IsValid)
            {

                var categories = await _categoryService.TListAllAsync();
                model.Categories = categories.Select(x => new SelectListItem
                {
                    Value = x.CategoryID.ToString(),
                    Text = x.CategoryName
                }).ToList();

                return View(model); 
            }

            string blogImagePath = null;
            if (model.BlogImageFile != null && model.BlogImageFile.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.BlogImageFile.FileName);
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.BlogImageFile.CopyToAsync(fileStream);
                }

                blogImagePath = "/images/" + uniqueFileName;
            }

            var blog = new Blog
            {
                BlogTitle = model.BlogTitle,
                BlogImage = blogImagePath,
                BlogContent = model.BlogContent,
                CategoryID = model.CategoryID,
                BlogStatus = true,
                BlogCreateDate = DateTime.UtcNow,
                WriterID = writerId
            };

            await _blogService.TAddAsync(blog);

            return RedirectToAction("BlogListByWriter", "Blog");
        }
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> DeleteBlog(int id)
        {
            await _blogService.TDeleteAsync(id);
            return RedirectToAction("BloglistByWriter");
        }
        [Authorize(Roles = "Writer")]
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
        [Authorize(Roles = "Writer")]
        [HttpPost]
        public async Task<IActionResult> EditBlog(BlogUpdateViewModel model, IFormFile BlogImageFile, IFormFile BlogThumbnailFile)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var writer = await _writerService.GetWriterByUserIdAsync(user.Id);
            var writerId = writer.WriterID;

            var blog = await _blogService.TGetByIdAsync(model.BlogID);

            if (blog != null)
            {
                if (BlogThumbnailFile != null)
                {
                    var thumbnailPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", BlogThumbnailFile.FileName);
                    using (var stream = new FileStream(thumbnailPath, FileMode.Create))
                    {
                        await BlogThumbnailFile.CopyToAsync(stream);
                    }
                    blog.BlogThumbnailImage = "/images/" + BlogThumbnailFile.FileName; 
                }

                if (BlogImageFile != null)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", BlogImageFile.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await BlogImageFile.CopyToAsync(stream);
                    }
                    blog.BlogImage = "/images/" + BlogImageFile.FileName; 
                }

                
                blog.BlogTitle = model.BlogTitle;
                blog.BlogContent = model.BlogContent;
                blog.CategoryID = model.CategoryID;
                blog.WriterID = writerId;
                blog.BlogCreateDate = DateTime.UtcNow;
                blog.BlogStatus = true;

                await _blogService.TUpdateAsync(blog);
            }

            return RedirectToAction("BlogListByWriter");
        }


    }

}
