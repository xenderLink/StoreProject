using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Store.Data;
using Store.Models;

namespace Store;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllersWithViews();

        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

        builder.Services.AddDbContext<StoreDbContext>(options=>
           options.UseNpgsql(
           builder.Configuration.GetConnectionString("Store")) );

        builder.Services.AddIdentity<StoreUser, IdentityRole>()
                        .AddEntityFrameworkStores<StoreDbContext>();

        builder.Services.AddScoped<IProductsRepository, EFProductsRepository>();                
        builder.Services.AddScoped<ICategoriesRepository, EFCategoriesRepository>();
        builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();
        builder.Services.AddScoped<IImageRepository, EFImageRepository>();
        
        builder.Services.AddScoped<Cart>(sp=>SessionCart.GetCart(sp));
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options=>
        {   
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.IsEssential = true;
        });

        builder.Services.ConfigureApplicationCookie(options=>
        {
            options.LoginPath = "/Login";
            options.LogoutPath = "/Logout";
            options.AccessDeniedPath = "/Login";
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
        });

        var app = builder.Build();  

        app.UseRequestLocalization(options=>
        {
             options.AddSupportedCultures("ru-RU")
                    .AddSupportedUICultures("ru-RU")
                    .AddSupportedCultures("en-US")
                    .AddSupportedUICultures("en-US")
                    .SetDefaultCulture("ru-RU");
        });

        app.UseStatusCodePagesWithReExecute("/Error", "?statusCode={0}"); //кастомный обработчик ошибки 404

        await EnsurePopulated (app); 

        //app.UseHttpsRedirection(); для демонстрации проекта не нужен

        app.UseStaticFiles(); //wwwroot 

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider
                 (Path.Combine(Directory.GetCurrentDirectory(), "../Images") ),
            RequestPath = "/myfiles"               
        });
        
        app.UseSession();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute("default", 
         "Home/",
          new {Controller = "Home", action = "Index"});

        app.MapDefaultControllerRoute();

        app.MapControllers();
        app.Run();
    }

    public static async Task EnsurePopulated(IApplicationBuilder app)
    {
       await IdentitySeedData.SeedIdentityDataAsync(app);
       await SeedData.SeedDataAsync(app);
    }
}