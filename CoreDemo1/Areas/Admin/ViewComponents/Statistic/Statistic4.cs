using BusinessLayer.Abstract;
using DocumentFormat.OpenXml.Office2013.Word;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Areas.Admin.ViewComponents.Statistic
{

    public class Statistic4 : ViewComponent
    {
        private readonly IAdminService _adminService;
        public Statistic4(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _adminService.TGetByIdAsync(1);
            return View(values);
        }
    }
}
