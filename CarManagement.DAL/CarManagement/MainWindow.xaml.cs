using BusinessObject.Models;
using CarManagement.BLL;
using CarManagement.BLL.Services;
using CarManagement.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Windows;

namespace CarManagement
{
    public partial class MainWindow : Window
    {
        private CarServices _carService;

        public MainWindow()
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

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            LoadCars();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var form = new CarForm();
            if (form.ShowDialog() == true)
            {
                _carService.CreateCar(form.Car);
                LoadCars();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dgCars.SelectedItem is Car selectedCar)
            {
                var form = new CarForm(selectedCar);
                if (form.ShowDialog() == true)
                {
                    _carService.UpdateCar(form.Car);
                    LoadCars();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn xe cần sửa.");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgCars.SelectedItem is Car car)
            {
                _carService.DeleteCar(car.Id);
                LoadCars();
            }
        }


    }
}
