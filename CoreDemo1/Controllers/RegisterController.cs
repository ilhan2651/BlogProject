using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IWriterService _writerService;
        public RegisterController(IWriterService writerService)
        {
            _writerService = writerService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Writer w)
        {
            w.WriterStatus = true;
            w.WriterAbout = "Deneme Test";
            await _writerService.TAddAsync(w);
            return RedirectToAction("Index","Blog");
        }
    }
}
