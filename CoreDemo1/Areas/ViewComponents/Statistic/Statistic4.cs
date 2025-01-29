using BusinessLayer.Abstract;
using DocumentFormat.OpenXml.Office2013.Word;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Areas.ViewComponents.Statistic
{
    
    public class Statistic4 : ViewComponent
    {
        private readonly IAdminService _adminService;
        public Statistic4(IAdminService adminService)
        {
            _adminService= adminService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.AdminName = await _adminService.GetAdminNameAsync(1);
            return View();
        }
    }
}
