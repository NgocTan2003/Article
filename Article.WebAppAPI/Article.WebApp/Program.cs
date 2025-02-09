using Article.WebApp.AutoConfiguration;
using Article.WebApp.ConnectAPI.ArticleAppUser;
using Article.WebApp.ConnectAPI.ArticleAppUserCnAPI;
using Article.WebApp.ConnectAPI.ArticleCategoryCnAPI;
using Article.WebApp.ConnectAPI.ArticleRole;
using Article.WebApp.ConnectAPI.ArticleRoleCnAPI;
using Article.WebApp.ConnectAPI.ArticleTokenCnAPI;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session tồn tại trong 30 phút
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddHttpClient();

builder.Services.AddScoped<TokenHandler>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddTransient<TokenHandler>();
builder.Services.AddScoped<IArticleAppUserConnectAPI, ArticleAppUserConnectAPI>();
builder.Services.AddScoped<IArticleAppRoleConnectAPI, ArticleAppRoleConnectAPI>();
builder.Services.AddScoped<IArticleCategoryConnectAPI, ArticleCategoryConnectAPI>();
builder.Services.AddTransient<IArticleTokenConnectAPI, ArticleTokenConnectAPI>();

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7085");
})
.AddHttpMessageHandler<TokenHandler>();

builder.Services.AddHttpClient("AuthClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7085");
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Authentication/Login"; // Đường dẫn đến trang đăng nhập
        options.LogoutPath = "/Authentication/Logout"; // Đường dẫn khi người dùng đăng xuất
        options.SlidingExpiration = true; // Hết hạn session khi không hoạt động
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Thời gian hết hạn của cookie
    });

var app = builder.Build();
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<TokenRefreshMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=ArticleCategory}/{action=Index}/{id?}");

});

app.Run();
