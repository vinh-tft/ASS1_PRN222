using BusinessObject.Models;
using CarManagement.BLL.Services;
using CarManagement.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;

namespace CarManagement
{
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();


            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();


            var connectionString = configuration.GetConnectionString("CarDealerDB");

            services.AddDbContext<CarDealerDbContext>(options =>
                options.UseSqlServer(connectionString));


            services.AddScoped<UserRepo>();
            services.AddScoped<UserServices>();

            ServiceProvider = services.BuildServiceProvider();

            var loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}
