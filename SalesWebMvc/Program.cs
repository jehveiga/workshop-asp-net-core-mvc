using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SalesWebMvcContext>(options =>
    options.UseMySql("server=localhost;initial catalog=saleswebmvcappdb;uid=developer;password=12345", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<SeedingService>(); //Add to the Service an application dependency injection

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedingService.Initialize(services);
}

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
