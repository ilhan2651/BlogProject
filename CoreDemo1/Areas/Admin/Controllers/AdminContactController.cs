using BusinessLayer.Abstract;
using DataAccessLayer.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class AdminContactController : Controller
    {
        private readonly IContactService _contactService;
        public AdminContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        public  async Task<IActionResult> Index()
        {
            var contactMessages = await _contactService.TListAllAsync();
            return View(contactMessages);
        }
        public async Task<IActionResult> DeleteContact(int id)
        {
            var valueDelete = await _contactService.TGetByIdAsync(id);
            if (valueDelete != null)
            {
                await _contactService.TDeleteAsync(id);
            }
            return RedirectToAction("Index");
        }
    }
}
