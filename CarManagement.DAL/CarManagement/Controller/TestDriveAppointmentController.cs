using BusinessObject.Models;
using CarManagement.BLL.Services;
using System.Windows;

namespace CarManagement.Controller
{
    public class TestDriveAppointmentController
    {
        private readonly TestDriveAppointmentService _service;
        private readonly CustomerService _customerService;
        private readonly CarServices _carService;
        private readonly TestDriveAppointmentView _view; // View WPF

        public TestDriveAppointmentController(
            TestDriveAppointmentView view,
            TestDriveAppointmentService service,
            CustomerService customerService,
            CarServices carService)
        {
            _view = view;
            _service = service;
            _customerService = customerService;
            _carService = carService;
        }
        public void SelectAppointment(TestDriveAppointment appointment)
        {
            _view.PopulateInputsFromSelection(appointment);
        }

        public void LoadAppointments()
        {
            var appointments = _service.GetAllAppointments();
            _view.LoadAppointmentsToGrid(appointments);
        }

        public void AddAppointment(TestDriveAppointment appointment)
        {
            try
            {
                appointment.CreatedAt = DateTime.Now;
                _service.AddAppointment(appointment);

                LoadAppointments();
                _view.ShowMessage("Appointment added successfully!", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Error adding appointment: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void UpdateAppointment()
        {
            try
            {
                var appointment = _view.GetAppointmentFromInputs();
                if (appointment == null || appointment.Id <= 0)
                {
                    _view.ShowMessage("Please select a valid appointment to update.",
                        "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _service.UpdateAppointment(appointment);

                LoadAppointments();
                _view.ShowMessage("Appointment updated successfully!", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Error updating appointment: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteAppointment()
        {
            try
            {
                int id = _view.GetSelectedAppointmentId();
                if (id <= 0)
                {
                    _view.ShowMessage("Please select a valid appointment to delete.",
                        "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var confirm = _view.ShowConfirm(
                    "Are you sure you want to delete this appointment?",
                    "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (confirm == MessageBoxResult.Yes)
                {
                    _service.DeleteAppointment(id);
                    LoadAppointments();
                    _view.ShowMessage("Appointment deleted successfully!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Error deleting appointment: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void HandleSelectionChanged(TestDriveAppointment selectedAppointment)
        {
            if (selectedAppointment != null)
            {
                _view.PopulateInputsFromSelection(selectedAppointment);
            }
        }

        // Lấy danh sách Customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _customerService.GetAllCustomers();
        }

        // Lấy danh sách Cars
        public IEnumerable<Car> GetCars()
        {
            return _carService.GetAllCars();
        }
    }
}
