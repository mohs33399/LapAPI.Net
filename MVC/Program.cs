using ConsoleApp1;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.Manegers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SchoolDbContext>(i =>
i.UseLazyLoadingProxies()
.UseSqlServer(builder.Configuration.GetConnectionString("School2Database")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<SchoolDbContext>();


builder.Services.AddScoped(typeof(TeacherManager));
builder.Services.AddScoped(typeof(AccountManager));
builder.Services.AddScoped(typeof(RoleManager));

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
