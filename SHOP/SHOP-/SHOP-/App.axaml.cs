using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using PIM.Data;
using SHOP;
using SHOP.Data;
using SHOP.Factories;
using SHOP.Interfaces;
using SHOP.Models;
using SHOP.Services;
using SHOP.ViewModels;

using SHOP.Views;

namespace SHOP_;

public partial class App : Application
{
    private ProductsAPI productsAPI;
    
    private ShopDbService _shopDbService;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    { 
        InitializeAsync();
       
        
        
        var collection = new ServiceCollection();
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddSingleton<HomePageViewModel>();
        collection.AddSingleton<ProductsPageViewModel>();
        collection.AddSingleton<ProductPageViewModel>();
        collection.AddSingleton<NavigationBarViewModel>();
        collection.AddSingleton<CategoryViewModel>();
        collection.AddSingleton<ICartService, CartService>();
        collection.AddSingleton<CartViewModel>();
        collection.AddSingleton<IShopDbService, ShopDbService>();


        
        collection.AddSingleton<PageFactory>();

        collection.AddSingleton<Func<ApplicationPageNames, PageViewModel>>(x => name => name switch
        {
            ApplicationPageNames.Home=> x.GetRequiredService<HomePageViewModel>(),
            ApplicationPageNames.Product => x.GetRequiredService<ProductPageViewModel>(),
           
            _ => throw new InvalidCastException(),
        });
        
        var services = collection .BuildServiceProvider();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindowView
            {
                DataContext = services.GetRequiredService<MainWindowViewModel>()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainWindowView()
            {
                DataContext = services.GetRequiredService<MainWindowViewModel>()
            };
        }
        base.OnFrameworkInitializationCompleted();
    }
    
    private async void InitializeAsync()
    {
        // Perform your async initialization here
        
        
      
        // database PIM connection:

        string connectionString = $"Host=localhost;Port=5432;Username={UserCredentials.username};Password={UserCredentials.password};Database= PIM";
        // string connectionString = $"Host=localhost;Port=5432;Username={UserCredentials.username};Password={UserCredentials.password};Database=Pim Clone"; // database laptop
 
        //var conn = new NpgsqlConnection(connectionString);
        //conn.Open();

        productsAPI = ProductsAPI.GetProductAPI();
        DatabaseConnection? dbConnection = new DatabaseConnection(connectionString);
        dbConnection.StartAsync();
        productsAPI.SetConnection(dbConnection);

        
       
         bool started =  await dbConnection.StartAsync().ConfigureAwait(false);
       
        if (!started || !dbConnection.isConnected())
        {
            // Handle connection failure (log error, show message, exit?)
            Console.Error.WriteLine("FATAL: Failed to start database connection!");
            // Optionally throw an exception or handle gracefully
            throw new Exception("Database connection could not be established.");
        }
        Console.WriteLine("PIM Database connection started successfully.");
        
        // 5. Set Connection in ProductsAPI
        productsAPI.SetConnection(dbConnection);
    }
    
    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}
