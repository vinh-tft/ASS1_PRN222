using System.Windows;
using CarManagement.BLL.Services;
using CarManagement.BLL.Services.Interface;
using CarManagement.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarManagement
{
    public partial class App : Application
    {
        // Service dùng toàn app
        public static ICarService CarService { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 👉 Chọn DB: SQLite (dễ chạy) hoặc SQL Server
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlite("Data Source=car.db") // SQLite file
                                                 //.UseSqlServer("Server=.;Database=CarDB;Trusted_Connection=True;TrustServerCertificate=True") // nếu muốn SQL Server
                .Options;

            var dbContext = new MyDbContext(options);

            // đảm bảo DB tạo và có dữ liệu seed
            dbContext.Database.EnsureCreated();

            // Khởi tạo repository + service
            var repo = new CarRepository(dbContext);
            CarService = new CarService(repo);
        }
    }
}
