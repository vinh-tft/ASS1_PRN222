using BusinessObject.Models;
using CarManagement.BLL.Services;
using CarManagement.Controller;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CarManagement.DAL.Repositories;

namespace CarManagement
{
    public partial class TestDriveAppointmentView : Window
    {
        private readonly TestDriveAppointmentController _controller;

        public TestDriveAppointmentView()
        {
            InitializeComponent();

            // Repository
            var appointmentRepo = new TestDriveAppointmentRepository(new CarDealerDbContext());
            var customerRepo = new CustomerRepository(new CarDealerDbContext());
            var carRepo = new CarRepo(new CarDealerDbContext());

            // Services
            var appointmentService = new TestDriveAppointmentService(appointmentRepo);
            var customerService = new CustomerService(customerRepo);
            var carService = new CarServices(carRepo);

            // Controller
            _controller = new TestDriveAppointmentController(this, appointmentService, customerService, carService);

            // Load dữ liệu combobox
            cbCustomer.ItemsSource = _controller.GetCustomers();
            cbCar.ItemsSource = _controller.GetCars();

            // Load danh sách ban đầu (sync)
            _controller.LoadAppointments();
        }


        // Load dữ liệu vào DataGrid
        public void LoadAppointmentsToGrid(IEnumerable<TestDriveAppointment> appointments)
        {
            dgAppointments.ItemsSource = appointments;
        }

        // Lấy dữ liệu từ input form
        public TestDriveAppointment? GetAppointmentFromInputs()
        {
            try
            {
                int id = 0;
                int.TryParse(txtId.Text, out id);

                return new TestDriveAppointment
                {
                    Id = id,
                    CustomerId = (int)cbCustomer.SelectedValue,
                    CarId = (int)cbCar.SelectedValue,
                    AppointmentDate = dpAppointmentDateTime.Value ?? DateTime.Now, // ✅ dùng Value
                    Note = txtNote.Text,
                    CreatedAt = string.IsNullOrWhiteSpace(txtCreatedAt.Text)
                        ? DateTime.Now
                        : DateTime.Parse(txtCreatedAt.Text)
                };
            }
            catch
            {
                return null;
            }
        }




        // Lấy ID appointment được chọn
        public int GetSelectedAppointmentId()
        {
            if (dgAppointments.SelectedItem is TestDriveAppointment selected)
            {
                return selected.Id;
            }
            return 0;
        }

        // Hiển thị dữ liệu khi chọn 1 dòng trong DataGrid
        public void PopulateInputsFromSelection(TestDriveAppointment appointment)
        {
            txtId.Text = appointment.Id.ToString();
            cbCustomer.SelectedValue = appointment.CustomerId;
            cbCar.SelectedValue = appointment.CarId;
            dpAppointmentDateTime.Value = appointment.AppointmentDate; // ✅ gán vào DateTimePicker
            txtNote.Text = appointment.Note;
            txtCreatedAt.Text = appointment.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
        }




        // ==== CRUD Buttons (Sync) ====
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var customers = _controller.GetCustomers();
                var cars = _controller.GetCars();

                var window = new TestDriveAppointmentWindow(customers, cars);
                if (window.ShowDialog() == true)
                {
                    var appointment = new TestDriveAppointment
                    {
                        CustomerId = window.SelectedCustomerId,
                        CarId = window.SelectedCarId,
                        AppointmentDate = window.AppointmentDateTime,
                        Note = window.Note,
                        CreatedAt = DateTime.Now
                    };

                    _controller.AddAppointment(appointment);

                    // ✅ Sau khi thêm thì load lại danh sách
                    _controller.LoadAppointments();
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Error: {ex.Message}", "Add Appointment",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            _controller.UpdateAppointment();

            // ✅ Refresh sau khi Update
            _controller.LoadAppointments();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            _controller.DeleteAppointment();

            // ✅ Refresh sau khi Delete
            _controller.LoadAppointments();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            _controller.LoadAppointments();
        }

        // Khi chọn trong DataGrid
        private void dgAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAppointments.SelectedItem is TestDriveAppointment appointment)
            {
                _controller.SelectAppointment(appointment);
            }
        }


        // Helpers
        public void ShowMessage(string message, string title, MessageBoxButton button, MessageBoxImage image)
        {
            MessageBox.Show(message, title, button, image);
        }

        public MessageBoxResult ShowConfirm(string message, string title, MessageBoxButton button, MessageBoxImage image)
        {
            return MessageBox.Show(message, title, button, image);
        }
    }
}
