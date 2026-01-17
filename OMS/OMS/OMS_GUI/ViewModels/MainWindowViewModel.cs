using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS_GUI.Views;
using OMS.Process;
using PIM;
using PIM.Data;

namespace OMS_GUI.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly Process processInstance;
        private readonly ProductsAPI productsAPI;
        private readonly string connString = "Host=localhost;Port=5432;Database=oms;Username=postgres;Password=1234";

        [ObservableProperty]
        private ViewModelBase _currentWindow;

        private readonly HomeWindowViewModel _homeWindow = new();
        private readonly CurrentOrderWindowViewModel _currentOrderWindow = new();
        private readonly OrderChangerWindowViewModel _orderChangerWindow = new();
        private readonly OrderFinderWindowViewModel _orderFinderWindow = new();
        private readonly SalesReportWindowViewModel _salesReportWindow = new();
        private readonly StockStatusWindowViewModel _stockStatusWindow = new();
        private readonly StockUpdateWindowViewModel _stockUpdateWindow = new();

        public MainWindowViewModel()
        {
            CurrentWindow = _homeWindow;

            productsAPI = ProductsAPI.GetProductAPI();

            processInstance = Process.GetInstance(connString, productsAPI);

            DatabaseConnection? connection = new DatabaseConnection(connString);



            try
            {
                connection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            productsAPI.SetConnection(connection);
        }

        [RelayCommand]
        private void GoToHome()
        {
            CurrentWindow = _homeWindow;
        }

        [RelayCommand]
        private void GoToCurrentOrder()
        {
            CurrentWindow = _currentOrderWindow;
        }

        [RelayCommand]
        private void GoToOrderChanger()
        {
            CurrentWindow = _orderChangerWindow;
        }

        [RelayCommand]
        private void GoToOrderFinder()
        {
            CurrentWindow = _orderFinderWindow;
        }

        [RelayCommand]
        private void GoToSalesReport()
        {
            CurrentWindow = _salesReportWindow;
        }

        [RelayCommand]
        private void GoToStockStatus()
        {
            CurrentWindow = _stockStatusWindow;
        }

        [RelayCommand]
        private void GoToStockUpdate()
        {
            CurrentWindow = _stockUpdateWindow;
        }
    }
}
