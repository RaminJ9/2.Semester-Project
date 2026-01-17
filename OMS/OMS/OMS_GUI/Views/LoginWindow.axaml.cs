using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using OMS_GUI.ViewModels;
using OMS_GUI.Views;

namespace OMS_GUI;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }
    
    private void OnLoginButtonClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // Basic login button functionality
        var id = EmployeeIdBox.Text;
        var pw = PasswordBox.Text;
        
        bool isLoginValid = (id == "admin" && pw == "password"); // Authorization checkpoint

        if (isLoginValid)
        {
            var mainWindow = new MainWindow(); // Initialize mainWindow and mainWindowViewModel
            var mainWindowViewModel = new MainWindowViewModel();

            mainWindow.DataContext = mainWindowViewModel; // Change datacontext
            mainWindow.Show(); // Show mainWindow
            
            Close(); // Close login window
        }
        else
        {
            // Show invalid login message
            ErrorMessage.Text = "Invalid username or password.";
            ErrorMessage.IsVisible = true;
        }
    }
}