using BusinessLayer.Abstract;
using CoreDemo1.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.Controllers
{
    [AllowAnonymous]
    public class RegisterUserController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IWriterService _writerService;
        private readonly UserManager<AppUser> _userManager;
        public RegisterUserController(UserManager<AppUser> userManager, IWriterService writerService, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _writerService = writerService;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserSignUpViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(userModel.Mail);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Bu e-posta adresi zaten kullanılıyor.");
                    return View(userModel);
                }

                AppUser user = new AppUser()
                {
                    Email = userModel.Mail,
                    UserName = userModel.UserName,
                    NameSurname = userModel.NameSurname,
                    ImageUrl = "/writer/assets/images/faces/face4.jpg"
                };

                var result = await _userManager.CreateAsync(user, userModel.Password);
                await _userManager.AddToRoleAsync(user, "Writer");

                if (result.Succeeded)
                {
                    var createdUser = await _userManager.FindByEmailAsync(userModel.Mail);
                    if (createdUser != null)
                    {
                        Writer newWriter = new Writer()
                        {
                            WriterMail = createdUser.Email,
                            WriterName = createdUser.NameSurname,
                            WriterUserName=createdUser.UserName,
                            WriterAbout = "Yeni yazar profili",
                            WriterStatus = true,
                            AppUserId = createdUser.Id, 
                            WriterImage= "/writer/assets/images/faces/face4.jpg",
                            WriterPassword = new PasswordHasher<Writer>().HashPassword(null, userModel.Password),
                            ConfirmPassword = new PasswordHasher<Writer>().HashPassword(null, userModel.Password)
                        };

                        await _writerService.TAddAsync(newWriter);
                    }

                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(userModel);
        }

    }
}
