using Microsoft.AspNetCore.Builder; 
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.Extensions.Hosting; 
using Microsoft.Extensions.Logging; 
using Microsoft.EntityFrameworkCore; 
using eShopLegacyMVC.Models; 
using eShopLegacyMVC.Services;
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args); 
// Add services to the container. 
builder.Services.AddControllersWithViews(); 
// Configure DbContext with SQL Server 
builder.Services.AddDbContext<CatalogDBContext>(options => 
{ 
    try 
    { 
        var connectionString = builder.Configuration.GetConnectionString("CatalogDB"); 
        if (string.IsNullOrEmpty(connectionString)) 
        { 
            throw new InvalidOperationException("Connection string 'CatalogDB' is not configured."); 
        } 
        options.UseSqlServer(connectionString); 
        Console.WriteLine("Successfully configured DbContext with SQL Server."); 
    } 
    catch (Exception ex) 
    { 
        Console.WriteLine($"Error configuring DbContext: {ex.Message}"); 
        Console.WriteLine(ex.StackTrace); 
        throw; 
    } 
}); 
// Register the services 
builder.Services.AddScoped<ICatalogService, CatalogService>(); // Use CatalogService or CatalogServiceMock as needed 
var app = builder.Build(); 
// Configure the HTTP request pipeline. 
if (!app.Environment.IsDevelopment()) 
{ 
    app.UseExceptionHandler("/Home/Error"); 
    app.UseHsts(); 
} 
app.UseHttpsRedirection(); 
app.UseStaticFiles(); 
app.UseRouting(); 
app.UseAuthorization(); 
app.MapControllerRoute( 
    name: "default", 
    pattern: "{controller=Catalog}/{action=Index}/{id?}"); 
app.Run();