using ConsoleApp1;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.Manegers;
using School.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<SchoolDbContext>(i =>
i.UseLazyLoadingProxies()
.UseSqlServer(builder.Configuration.GetConnectionString("School2Database")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<SchoolDbContext>();
builder.Services.AddScoped(typeof(TeacherManager));
builder.Services.AddScoped(typeof(AccountManager));
builder.Services.AddScoped(typeof(RoleManager));
builder.Services.AddScoped(typeof(AccountServices));



// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}



// Add services to the container.

//builder.Services.AddControllers(i =>
//{
//    i.Filters.Add<GenaralExceptionFilter>();
//});




builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    //on One Statless Request
    option.SaveToken = true;
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:PrivateKey"]))
    };
});

builder.Services.AddCors(option => option.AddDefaultPolicy
    (
        i => i.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseStaticFiles();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=index}");


app.Run();

