using PatientManagement.WPF.Services;
using System.Windows;
using System.Windows.Controls;


namespace PatientManagement.WPF.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly AuthService _auth;

        public LoginWindow()
        {
            InitializeComponent();

            var api = new ApiService();

            _auth = new AuthService(api);
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            var success = await _auth.Login(new()
            {
                Username = UsernameBox.Text,
                Password = PasswordBox.Password
            });

            if (success)
            {
                MainWindow main = new();

                main.Show();

                Close();
            }
            else
            {
                MessageBox.Show("Invalid credentials");
            }
        }
    }
}
