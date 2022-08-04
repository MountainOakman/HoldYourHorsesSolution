using HoldYourHorses.Models;
using HoldYourHorses.Models.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<DataService>();

builder.Services.Configure<CookiePolicyOptions>(options => {
	options.CheckConsentNeeded = context => true;
	options.MinimumSameSitePolicy = SameSiteMode.Strict;
});

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SticksDBContext>(o => o.UseSqlServer(connString));
builder.Services.AddSession();
var app = builder.Build();
app.UseSession();
app.UseCookiePolicy();
app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(o => o.MapControllers());
app.Run();
