using CarManagement.BLL.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CarManagement
{
    public partial class LoginWindow : Window
    {
        private readonly UserServices _userService;

        public LoginWindow()
        {
            InitializeComponent();
            _userService = App.ServiceProvider.GetService<UserServices>();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Password;

            var userServices = App.ServiceProvider.GetService<UserServices>();
            var user = userServices.Login(username, password);

            if (user != null)
            {
                if (user.Roles.Any(r => r.RoleName == "Admin" || r.RoleName == "EVMStaff"))
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền truy cập!");
                }
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
        }
    }
}
