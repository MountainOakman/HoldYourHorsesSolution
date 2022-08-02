using HoldYourHorses.Models;
using HoldYourHorses.Models.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<DataService>();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SticksDBContext>(o => o.UseSqlServer(connString));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(o => o.MapControllers());
app.Run();
