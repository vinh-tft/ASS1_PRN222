using BusinessObject.Models;
using System.Windows;

namespace CarManagement
{
    public partial class CarForm : Window
    {
        public Car Car { get; private set; }

        public CarForm(Car? car = null)
        {
            InitializeComponent();

            if (car != null)
            {
                Car = car;
                txtName.Text = car.Name;
                txtConfig.Text = car.Configuration;
                txtPrice.Text = car.Price.ToString();
                chkElectric.IsChecked = car.IsElectric;
            }
            else
            {
                Car = new Car();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Car.Name = txtName.Text;
                Car.Configuration = txtConfig.Text;
                Car.Price = decimal.Parse(txtPrice.Text);
                Car.IsElectric = chkElectric.IsChecked ?? false;

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
