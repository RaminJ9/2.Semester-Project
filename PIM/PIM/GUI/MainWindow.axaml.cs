using System;
using System.ComponentModel;
using Avalonia.Controls;
using PIM.Data;

namespace PIM;
public partial class MainWindow : Window, INotifyPropertyChanged
{
    ProductsAPI productsAPI;
    public MainWindow()
    {
        UserCredentials.credentials = new UserCredentials(UserCredentials.filePath); // Initialize credentials in constructor
        UserCredentials.ConnectionString =
            $"Host=localhost;Port=5432;Username={UserCredentials.username};Password={UserCredentials.credentials.GetPassword(UserCredentials.username)};Database=postgres";
        productsAPI = ProductsAPI.GetProductAPI();
        productsAPI.SetConnection( new DatabaseConnection(UserCredentials.ConnectionString));
        InitializeComponent();
    }

    
    //Method to make all tabs visible when after log in
    public void ShowAllTabs()
    {
        ImportExportTab.IsVisible = true;
        ViewProductsTab.IsVisible = true;
    }
    //Method to make all tabs visible when after log in
    public void HideAllTabs()
    {
        ImportExportTab.IsVisible = false;
        ViewProductsTab.IsVisible = false;
    }


}


