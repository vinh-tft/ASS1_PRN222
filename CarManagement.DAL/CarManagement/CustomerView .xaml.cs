using System.Windows;
using System.Windows.Controls;


namespace CarManagement
{
    public partial class CustomerView : Window
    {
        private readonly CustomerController _controller;

        public CustomerView()
        {
            InitializeComponent();
            _controller = new CustomerController(this); // Pass the view to the controller
            _controller.ViewCustomers(); // <-- Add this line

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddCustomerWindow
            {
                Owner = this
            };

            if (addWindow.ShowDialog() == true)
            {
                var customer = new BusinessObject.Models.Customer
                {
                    FullName = addWindow.FullName,
                    Phone = string.IsNullOrWhiteSpace(addWindow.Phone) ? null : addWindow.Phone,
                    Email = string.IsNullOrWhiteSpace(addWindow.Email) ? null : addWindow.Email,
                    CreatedAt = DateTime.Now
                };

                _controller.AddCustomer(customer);
            }
        }


        private void BtnView_Click(object sender, RoutedEventArgs e)
        {
            _controller.ViewCustomers();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            _controller.UpdateCustomer();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            _controller.DeleteCustomer();
        }

        private void dgCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _controller.HandleSelectionChanged();
        }

        // Public methods for the controller to update the UI
        public void LoadCustomersToGrid(IEnumerable<BusinessObject.Models.Customer> customers)
        {
            dgCustomers.ItemsSource = customers;
            ClearInputs();
        }

        public void ClearInputs()
        {
            txtId.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtCreatedAt.Text = string.Empty;
        }

        public void PopulateInputsFromSelection(BusinessObject.Models.Customer selectedCustomer)
        {
            txtId.Text = selectedCustomer.Id.ToString();
            txtFullName.Text = selectedCustomer.FullName;
            txtPhone.Text = selectedCustomer.Phone ?? string.Empty;
            txtEmail.Text = selectedCustomer.Email ?? string.Empty;
            txtCreatedAt.Text = selectedCustomer.CreatedAt.ToString("dd/MM/yyyy HH:mm");
        }

        public string GetFullName() => txtFullName.Text;
        public string GetPhone() => txtPhone.Text;
        public string GetEmail() => txtEmail.Text;
        public string GetIdText() => txtId.Text;

        public void ShowMessage(string message, string title, MessageBoxButton button, MessageBoxImage image)
        {
            MessageBox.Show(message, title, button, image);
        }

        public MessageBoxResult ShowConfirm(string message, string title, MessageBoxButton buttons, MessageBoxImage image)
        {
            return MessageBox.Show(message, title, buttons, image);
        }
    }
}