using BusinessObject.Models;
using System.Windows;

namespace CarManagement
{
    public partial class TestDriveAppointmentWindow : Window
    {
        public int SelectedCustomerId => (int)cbCustomer.SelectedValue;
        public int SelectedCarId => (int)cbCar.SelectedValue;

        // Lấy ngày giờ trực tiếp từ DateTimePicker
        public DateTime AppointmentDateTime => dtpAppointmentDateTime.Value ?? DateTime.Now;

        public string Note => txtNote.Text.Trim();

        public TestDriveAppointmentWindow(
            IEnumerable<Customer> customers,
            IEnumerable<Car> cars)
        {
            InitializeComponent();

            cbCustomer.ItemsSource = customers;
            cbCar.ItemsSource = cars;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cbCustomer.SelectedItem == null)
            {
                MessageBox.Show("Please select a customer.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (cbCar.SelectedItem == null)
            {
                MessageBox.Show("Please select a car.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (dtpAppointmentDateTime.Value == null)
            {
                MessageBox.Show("Please select an appointment date and time.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
