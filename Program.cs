using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using StudentLibrary.Data;
using StudentLibrary.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor(); // авторизация
builder.Services.AddSignalR(); // ChatHub

// Настройка подключения к базе данных
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentLibraryDb")));

// авторизация ++ 
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Account/Login"; // путь к странице входа
        options.AccessDeniedPath = "/Account/AccessDenied"; // опционально — если доступ запрещен
    });

// авторизация --

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

app.MapHub<ChatHub>("/chatHub"); // ChatHub

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();// авторизация

app.UseStaticFiles();
app.MapStaticAssets();
app.MapRazorPages();
app.MapControllers();

app.Run();