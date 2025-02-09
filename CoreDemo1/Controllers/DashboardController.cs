using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.BaseRepository.Concrete;
using DocumentFormat.OpenXml.Spreadsheet;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Writer")]

public class DashboardController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IWriterService _writerService;
    private readonly ICategoryService _categoryService;
    private readonly IBlogService _blogService;

    public DashboardController(IWriterService writerService,ICategoryService categoryService, UserManager<AppUser> userManager, IBlogService blogService)
    {
        _userManager = userManager;
        _writerService = writerService;
        _categoryService = categoryService;
        _blogService = blogService;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var writer = await _writerService.GetWriterByUserIdAsync(user.Id);
        var writerId = writer.WriterID;

        ViewBag.v1 = await _blogService.GetTotalBlogsCountAsync();
        ViewBag.v2 = await _blogService.GetWriterBlogCountAsync(writerId);
        ViewBag.v3 = await _categoryService.GetCategoriesCountAsync();
        return View();
    }
}
