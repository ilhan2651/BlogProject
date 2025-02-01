using CoreDemo1.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Chart")]
    public class ChartController : Controller
    {
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("CategoryChart")]
        public IActionResult CategoryChart()
        {
            var list = new List<CategoryClass>
            {
                new CategoryClass { categoryName = "Teknoloji", categoryCount = 10 },
                new CategoryClass { categoryName = "Yazılım", categoryCount = 14 },
                new CategoryClass { categoryName = "Spor", categoryCount = 5 },
                new CategoryClass { categoryName = "Sinema", categoryCount = 23 },
                new CategoryClass { categoryName = "Zeka Oyunları", categoryCount = 8 }



            };

            return Json(list);
        }
    }
}
