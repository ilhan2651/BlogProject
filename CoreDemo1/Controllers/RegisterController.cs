using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IWriterService _writerService;
        private readonly IValidator<Writer> _validator;
        public RegisterController(IWriterService writerService,IValidator<Writer> validator)
        {
            _writerService = writerService;
            _validator = validator;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Writer w)
        {

            var validationResult=await _validator.ValidateAsync(w);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName,error.ErrorMessage);
                }
                return View();
            }

            w.WriterStatus = true;
            w.WriterAbout = "Deneme Test";
            await _writerService.TAddAsync(w);
            return RedirectToAction("Index","Blog");
        }
    }
}
