using BusinessLayer.Abstract;
using ClosedXML.Excel;
using CoreDemo1.Areas.Admin.Models;
using DataAccessLayer.BaseRepository.Concrete;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Areas.Admin.Controllers
{ 
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly BlogProjectContext _context;
        
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService, BlogProjectContext context)
        {
            _blogService = blogService;
            _context = context;
        }
        public IActionResult ExportStaticExcelBlogList()
        {
            using (var workBook = new XLWorkbook())
            {
                var workSheets = workBook.Worksheets.Add("BlogListesi");
                workSheets.Cell(1, 1).Value = "Blog ID";
                workSheets.Cell(1, 2).Value = "Blog Adı";

                int blogRowCount = 2;
                foreach (var item in GetBlogList())
                {
                    workSheets.Cell(blogRowCount,1).Value=item.ID;
                    workSheets.Cell(blogRowCount,2).Value=item.BlogName;
                    blogRowCount++;
                }
                using (var stream = new MemoryStream()) 
                {
                    workBook.SaveAs(stream);
                    var content=stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","Calisma1.xlsx");
                }
            }
        }
        public List<BlogModel> GetBlogList()
        {
            List<BlogModel> bm= new List<BlogModel>
            {
                new BlogModel { ID = 1, BlogName = "C# programlamaya giriş" },
                new BlogModel { ID = 2, BlogName = "Tesla Firmasının Araçları" },
                new BlogModel { ID = 3, BlogName = "2022 olimpiyatları" }
            };
            return bm;
        }
        public async Task<IActionResult> BlogListExcel()
        {
            return View();
        }
        public async Task<IActionResult> ExportDynamicExcelBlogList()
        {
            using (var workBook = new XLWorkbook())
            {
                var workSheets = workBook.Worksheets.Add("Blog Listesi");
                workSheets.Cell(1, 1).Value = "Blog ID";
                workSheets.Cell(1, 2).Value = "Blog Adı";

                int blogRowCount = 2;
                foreach (var item in BlogTitleList())
                {
                    workSheets.Cell(blogRowCount, 1).Value = item.ID;
                    workSheets.Cell(blogRowCount, 2).Value = item.BlogName;
                    blogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Calisma1.xlsx");
                }
                
            }
        }
        public List<BlogModel2> BlogTitleList()
        {
            List<BlogModel2> blogModels = new List<BlogModel2>();
            blogModels=_context.Blogs.Select(x=> new BlogModel2
            {
                ID=x.BlogID,
                BlogName=x.BlogTitle
            }).ToList();
          return blogModels;
        }
        public async Task<IActionResult> BlogTitleListExcel()
        {
            return View();
        }
    }
}
