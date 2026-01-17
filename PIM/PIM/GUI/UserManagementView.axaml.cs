using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Npgsql;

namespace PIM;

public partial class UserManagementView : UserControl
{
    public UserManagementView()
    {
        InitializeComponent();
    }
        public static async Task<NpgsqlConnection> GetConnectionAsync()
    {
        var connection = new NpgsqlConnection(UserCredentials.ConnectionString);
        await connection.OpenAsync();
        return connection;
    }
    //back button
    private void BackButton(object? sender, RoutedEventArgs e)
    {
        var main = (MainWindow)this.VisualRoot;
        var tabControl = main.FindControl<TabControl>("MainTabs");
        tabControl.SelectedIndex = 0; // 0 = Login tab
    }


    // Add user button
    private async void Button_Add_User(object? sender, RoutedEventArgs e)
    {
        string username = Username_Add.Text;
        string password = Password_Add.Text;
        
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || password.Length < 8)
        {
            outputAdd.Text = "-Username or password can't be\n empty or have whitespaces  \n-Password must be at least 8\n characters long.";
            return;
        }

        string secretCode = SupervisorCode.Text;

        if (secretCode == "Arnes_Store")
        {
            await Authentication.InsertUserAsync(username, password); // Add user and password to database 
            outputAdd.Text = "User added to database: \n" + username;
            Username_Add.Text = "";
            Password_Add.Text = "";
            SupervisorCode.Text = "";
        }
        else
        {
            outputAdd.Text = "WRONG Secret Code";
            SupervisorCode.Text = "";
        }
    }

    // Delete user
    private async void Button_Delete_User(object? sender, RoutedEventArgs e)
    {
        var users = await Authentication.GetAllUsersAsync(); // Retrieve all users 
        string username = Username_Delete.Text;

        // Is user in database
        if (users.Contains(username))
        {
            string secretCode = SupervisorCode_Delete.Text;
            if (secretCode == "Arnes_Store")
            {
                await Authentication.DeleteUserAsync(username); // Delete user
                outputAdd_Delete.Text = "User Removed From Database: \n" + username;
                Username_Delete.Text = "";
                SupervisorCode_Delete.Text = "";
            }
            else
            {
                outputAdd_Delete.Text = "WRONG Secret Code";
                SupervisorCode_Delete.Text = "";
            }
        }
        else
        {
            outputAdd_Delete.Text = "User does not exist!";
        }
    }

    
}