using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.BaseRepository.Abstract;
using DataAccessLayer.BaseRepository.Concrete;
using DataAccessLayer.Repositories.Abstract;
using DataAccessLayer.Repositories.Concrete;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BlogProjectContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddControllersWithViews();

builder.Services.AddFluentValidationAutoValidation()  // Otomatik ModelState validasyonu
                .AddFluentValidationClientsideAdapters();  // Client-side validasyon

builder.Services.AddValidatorsFromAssemblyContaining<BlogValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<WriterValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryValidator>();

builder.Services.AddTransient<IValidator<Writer>, WriterValidator>();

builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));
builder.Services.AddScoped<IAboutRepository, AboutRepository>();
builder.Services.AddScoped<IAboutService, AboutManager>();

builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IBlogService, BlogManager>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentManager>();

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactManager>();

builder.Services.AddScoped<IWriterRepository, WriterRepository>();
builder.Services.AddScoped<IWriterService, WriterManager>();

builder.Services.AddScoped<INewsletterRepository, NewsletterRepository>();
builder.Services.AddScoped<INewsletterService, NewsletterManager>();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationManager>();

builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageService, MessageManager>();

builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminService, AdminManager>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<BlogProjectContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/Index";  // **Giriþ yapmamýþ kullanýcýlarý buraya yönlendir**
    options.AccessDeniedPath = "/Login/AccessDenied"; // **Yetkisiz eriþimler buraya gitsin**
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);  // Oturum süresini 60 dakika yap
});

// **Tüm sayfalara yetkilendirme zorunlu!**
builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
                      .RequireAuthenticatedUser()
                      .Build();
    options.Filters.Add(new AuthorizeFilter(policy)); // **Tüm sayfalara yetkilendirme ekle**
    options.Filters.Add(new IgnoreAntiforgeryTokenAttribute());
});

builder.Services.AddMvc();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(z =>
    {
        z.LoginPath = "/Login/Index"; // **Giriþ yapmamýþ kullanýcýlar buraya yönlendirilecek**
    });

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// **Middleware sýrasýný kontrol et**
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error1", "?code={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); // **Kimlik doðrulama middleware'i buraya eklendi**
app.UseAuthorization();  // **Yetkilendirme middleware'i**

app.Use(async (context, next) =>
{
    Console.WriteLine($"Yönlendirme: {context.Request.Path}");
    await next.Invoke();
});

app.UseEndpoints(endpoints =>
{
    // Area route
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    // Default route
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
