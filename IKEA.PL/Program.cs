using IKEA.DAL.Persistance.Data;
using IKEA.DAL.Persistance.Repositories.Departments;
using Microsoft.EntityFrameworkCore;

namespace IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Entry Point
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Services

            builder.Services.AddControllersWithViews();

            #region The best Way to Configure Contexts 
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
            });
            #endregion

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            #region The New Way to Configure Contexts , Also Not Secure Way
            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseSqlServer("Server=DESKTOP-A1088VT\\ABDOHAMDY;Database=IKEA;Trusted_Connection=true;TrustServerCertificate=true");
            //});
            #endregion

            #region Old Way to Configure Contexts , Not Secure Way 

            //builder.Services.AddScoped<ApplicationDbContext>();
            //builder.Services.AddScoped<DbContextOptions<ApplicationDbContext>>((service) =>
            //{
            //    var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //    optionBuilder.UseSqlServer("Server=DESKTOP-A1088VT\\ABDOHAMDY;Database=IKEA;Trusted_Connection=true;TrustServerCertificate=true");

            //    var options = optionBuilder.Options;
            //    return options;
            //}); 
            #endregion

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            #region Configure Pipelines (Middle Wares)

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

            #endregion

            app.Run();
        }
    }
}
