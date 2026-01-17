using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PIM.Data;
using SHOP.Data;
using SHOP.Interfaces;
using SHOP.Models;
using SHOP.Services;
using System.Collections.Generic;
using System.Linq;

//using OMS.DataTypes;



namespace SHOP.ViewModels;

public partial class CartViewModel : PageViewModel
{
    public ICartService CartService { get; }
    
    [ObservableProperty] private ObservableCollection<KeyValuePair<ProductModel, int>> _productslist;
    
    [ObservableProperty] private float _totalPrice;
    
    //[ObservableProperty] private Customer _myCustomer;
    
    private readonly MainWindowViewModel _main;


    public string EstimatedDelivery => $"Estimated Delivery: {DateTime.Now.AddDays(1):dd MMMM yyyy}";
    
    public CartViewModel() : base(ApplicationPageNames.Cart)
    {
        
    }

    public CartViewModel(MainWindowViewModel main, ICartService cartService) : this()
    {
        CartService = cartService;
        /*MyCustomer = new Customer(
            CustomerType.Private,
            "Emilio",
            "Jaijeh",
            "EmilioJaijeh@gmail.com",
            1, 
            new Address("Maniak street", 60821, "Muzambik", "Madrid")
            );*/

        _main = main;

    }

    [RelayCommand]
    public async Task LoadProductsAsync()
    {
        var productsList = await CartService.GetItemsList(); // Await the async method to get the list
        Productslist = new ObservableCollection<KeyValuePair<ProductModel, int>>((IEnumerable<KeyValuePair<ProductModel, int>>)productsList); // Convert List to ObservableCollection
        var total = await CartService.getTotal();
        TotalPrice = total;
    }

    [RelayCommand]
    public async Task RemoveItem(int sku)
    {
        CartService.RemoveItem(sku);
        
        foreach (var p in Productslist)
        {
            if(p.Key.SKU == sku)
            {
                Productslist.Remove(p);
                break;
            }
        }
        
        var total = await CartService.getTotal();
        TotalPrice = total;
        
        
    }
    
    
    [RelayCommand]
    public async Task Checkout()
    {
        // Get total BEFORE resetting (if you want accurate summary before wipe)
        var total = await CartService.getTotal();
        TotalPrice = total;

        // Build product summary BEFORE resetting list
        var productSummaries = Productslist.Select(p =>
            $"{p.Key.Name} x{p.Value} - {(p.Key.SalesPrice * p.Value):C}"
        ).ToList();

        

        // Create and populate dialog view model
       /* var orderConfirmationDialog = new OrderConfirmationDialogViewModel("Your order has been placed successfully!")
        {
            ProductSummary = new ObservableCollection<string>(productSummaries),
            OrderId = Guid.NewGuid().ToString().Substring(0, 8).ToUpper()
     
       };

        */
        // Reset cart *after* capturing the data
        CartService.ResetCart();
        Productslist = new ObservableCollection<KeyValuePair<ProductModel, int>>();

        // Show dialog (make sure this binds to the populated VM)
        await _main.ShowOrderDialogAsync(TotalPrice, Guid.NewGuid().ToString().Substring(0, 8).ToUpper());
    }
    
    
    
    
}