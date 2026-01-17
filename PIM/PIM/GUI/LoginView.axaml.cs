using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System.ComponentModel;

namespace PIM
{
    public partial class LoginView : UserControl, INotifyPropertyChanged
    {
        private int _timesWrong = 0;

        public LoginView()
        {
            InitializeComponent();
        }
        
        private void UserManagementButton(object? sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)TopLevel.GetTopLevel(this);
            var tabControl = mainWindow.FindControl<TabControl>("MainTabs");
            tabControl.SelectedIndex = 1; // Index of the User Management tab
        }
        
        // Log in
        private async void LoginButton_Click(object? sender, RoutedEventArgs e)
        {
            var users = await Authentication.GetAllUsersAsync();
            var passwords = await Authentication.GetAllPasswordsAsync();

            string username = Username_LogIn.Text;
            string password = Password_LogIn.Text;

            // If user and password matches log in
            if (users.Contains(username) && passwords.Contains(password))
            {
                _timesWrong = 0;

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    outputLogIn.Text = "Logged In Successfully";
                    Username_LogIn.Text = "";
                    Password_LogIn.Text = "";
                    Button_LogIn.IsEnabled = false;
                    Button_LogOut.IsEnabled = true;
                    UserButton.IsEnabled = true;
                    Username_LogIn.IsEnabled = false;
                    Password_LogIn.IsEnabled = false;
                    // Access MainWindow and call method
                    var mainWindow = (MainWindow)TopLevel.GetTopLevel(this);
                    mainWindow.ShowAllTabs();
                });
            }
            else
            {
                _timesWrong++;

                outputLogIn.Text = _timesWrong switch
                {
                    1 => "Wrong username or password!\n3 TRIES LEFT!",
                    2 => "Wrong username or password!\n2 TRIES LEFT!",
                    3 => "Wrong username or password!\n1 TRY LEFT!",
                    _ => "You can't log in now,\n contact your supervisor!"
                };

                if (_timesWrong >= 4)
                {
                    Button_LogIn.IsEnabled = false;
                    Password_LogIn.IsEnabled = false;
                    Username_LogIn.IsEnabled = false;
                    Supervisor_Code.IsEnabled = true;
                    SecretCodeApply.IsEnabled = true;
                }
            }
        }
        

        // Types wrong 3+ times
        private void SecretCode_Click(object? sender, RoutedEventArgs e)
        {
            if (Supervisor_Code.Text == "Arnes_Store")
            {
                Button_LogIn.IsEnabled = true;
                Password_LogIn.IsEnabled = true;
                Button_LogOut.IsEnabled = false;
                Username_LogIn.IsEnabled = true;
                Supervisor_Code.IsEnabled = false;
                SecretCodeApply.IsEnabled = false;
                UserButton.IsEnabled = true;

                Supervisor_Code.Text = "";
                Username_LogIn.Text = "";
                Password_LogIn.Text = "";
                outputLogIn.Text = "";
                _timesWrong = 0;
            }
            else
            {
                Supervisor_Code.Text = "";
                outputLogIn.Text = "WRONG Secret Code";
            }
        }

        // Log out
        private async void LogOutButton_Click(object? sender, RoutedEventArgs e)
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                outputLogIn.Text = "Logged Out Successfully";
                Supervisor_Code.Text = "";
                Username_LogIn.Text = "";
                Password_LogIn.Text = "";
                Button_LogIn.IsEnabled = true;
                Button_LogOut.IsEnabled = false;
                UserButton.IsEnabled = false;
                Username_LogIn.IsEnabled = true;
                Password_LogIn.IsEnabled = true;
                // Access MainWindow and call method
                var mainWindow = (MainWindow)TopLevel.GetTopLevel(this);
                mainWindow.HideAllTabs();
            });
            
        }
    
    }
}