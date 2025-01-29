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
builder.Services.AddScoped<INewsletterService , NewsletterManager>();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationManager>();

builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageService, MessageManager>();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});



builder.Services.AddControllersWithViews(options =>
{
	var policy = new AuthorizationPolicyBuilder()
					  .RequireAuthenticatedUser()
					  .Build();
	options.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddMvc();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(z =>
    {
        z.LoginPath = "/Login/Index";
    });

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/ErrorPage/Error1","?code={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

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
