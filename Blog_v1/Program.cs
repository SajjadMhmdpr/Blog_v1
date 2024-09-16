using Blog.CoreLayer.Services.Categories;
using Blog.CoreLayer.Services.Comments;
using Blog.CoreLayer.Services.MainPage;
using Blog.CoreLayer.Services.Posts;
using Blog.CoreLayer.Services.Users;
using Blog.DataLayer.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<BlogContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("sql"));
});
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IMainPageService,MainPageService>();

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("AdminPolicy", builder =>
    {
        builder.RequireRole("Admin");
        //builder.RequireClaim("NameIdentifier","3");
        //builder.RequireUserName("sajjad");
    });
});

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(option =>
{
    option.ExpireTimeSpan = TimeSpan.FromDays(1);
    option.LoginPath = "/Auth/Login";
    option.LogoutPath = "/Auth/Logout";
    option.AccessDeniedPath = "/searchResult";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/ErrorHandler/500");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseExceptionHandler("/ErrorHandler/500");
app.UseStatusCodePagesWithReExecute("/ErrorHandler/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

/*app.MapAreaControllerRoute(
    "myArea",
    "Admin",
    "{Controller=Home}/{action=Index}/{id?}");*/
//app.MapControllerRoute(
//    "myArea",
//    "{area:exist}/{Controller=Home}/{action=Index}/{id?}");
//app.UseEndpoints(endpoints =>
//{
//    //endpoints.MapAreaControllerRoute(
//    //    name: "AdminArea",
//    //    areaName: "Admin",
//    //    pattern: "D/{controller=Home}/{action=Index}/{id?}"
//    //);

//    // other areas configurations go here 

//    //endpoints.MapControllerRoute(
//    //    name: "default",
//    //    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    //);
//});

app.MapControllerRoute(
    "myArea",
    "{area:exists}/{Controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
