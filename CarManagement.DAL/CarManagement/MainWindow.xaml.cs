using System.Windows;
using CarManagement.BLL.Services;
using CarManagement.BLL.Services.Interface;
using CarManagement.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarManagement
{
    public partial class App : Application
    {
        // Service dùng chung cho toàn bộ app
        public static ICarService CarService { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 👉 Kết nối SQL Server
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlServer("Server=.;Database=CarDB;Trusted_Connection=True;TrustServerCertificate=True")
                .Options;

            var dbContext = new MyDbContext(options);

            // Tạo DB và seed data nếu chưa có
            dbContext.Database.EnsureCreated();

            // Khởi tạo repository + service
            var repo = new CarRepository(dbContext);
            CarService = new CarService(repo);
        }
    }
}
