using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using Services.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<OnlineMovieBookingApplicationContext>
    (tm => tm.UseSqlServer(builder.Configuration.GetConnectionString("OnlineMovieBookingConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ILoginRepository, LoginRepository>();
builder.Services.AddTransient<IMovieRepository,  MovieRepository>();
//builder.Services.AddTransient<IGenreRepository, GenreRepository>();
builder.Services.AddTransient<IBookingRepository, BookingRepository>();
builder.Services.AddTransient<IPaymentDetailsRepository, PaymentDetailsRepository>();

var rotativaPath = builder.Configuration["Rotativa:Path"];
RotativaConfiguration.Setup(rotativaPath);

//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<OnlineMovieBookingApplicationContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(defaultScheme: "Cookies")
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login";
        options.AccessDeniedPath = "/Login/Login/";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
