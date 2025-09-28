using BusinessObject.Models;
using CarManagement.BLL.Services;
using CarManagement.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CarManagement
{
    public partial class StaffWindow : Window
    {
        private readonly CarServices _carService;

        public StaffWindow()
        {
            InitializeComponent();

            var options = new DbContextOptionsBuilder<CarDealerDbContext>()
                .UseSqlServer("Server=localhost;Database=CarDealerDB;Trusted_Connection=True;TrustServerCertificate=True;")
                .Options;

            var context = new CarDealerDbContext(options);
            var repo = new CarRepo(context);
            _carService = new CarServices(repo);

            LoadCars();
        }

        private void LoadCars()
        {
            List<Car> cars = _carService.GetAllCars();
            dgCars.ItemsSource = cars;
        }
        private void dgCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCars = dgCars.SelectedItems.Cast<Car>().ToList();

      
            if (selectedCars.Count > 2)
            {

                foreach (var item in e.AddedItems)
                {
                    dgCars.SelectedItems.Remove(item);
                    break; 
                }
                return;
            }

            if (selectedCars.Count > 0)
                Car1Info.Text = FormatCar(selectedCars[0]);
            else
                Car1Info.Text = "";

            if (selectedCars.Count > 1)
                Car2Info.Text = FormatCar(selectedCars[1]);
            else
                Car2Info.Text = "";
        }


        private string FormatCar(Car car)
        {
            return $"ID: {car.Id}\n" +
                   $"Name: {car.Name}\n" +
                   $"Configuration: {car.Configuration}\n" +
                   $"Price: {car.Price:C}\n" +
                   $"Is Electric: {(car.IsElectric ? "Yes" : "No")}";
        }

        private void btnBackToLogin_Click(object sender, RoutedEventArgs e)
        {
            var options = new DbContextOptionsBuilder<CarDealerDbContext>()
                .UseSqlServer("Server=localhost;Database=CarDealerDB;Trusted_Connection=True;TrustServerCertificate=True;")
                .Options;

            var context = new CarDealerDbContext(options);
            var userRepo = new UserRepo(context);
            var userServices = new UserServices(userRepo);

            var loginWindow = new LoginWindow(userServices);
            loginWindow.Show();
            this.Close();
        }

    }
}
