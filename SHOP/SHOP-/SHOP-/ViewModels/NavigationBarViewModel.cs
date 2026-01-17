using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PIM.Data;
using PIM.Models;
using SHOP.Data;
using SHOP.Factories;
using SHOP.Interfaces;


namespace SHOP.ViewModels;

public partial class NavigationBarViewModel : ViewModelBase
{
    private ProductsAPI productsAPI;
    
    private readonly MainWindowViewModel _main;
    private PageFactory _pageFactory;
    private ICartService cartService;

    public NavigationBarViewModel(MainWindowViewModel main, ICartService cartService)
    {
        _main = main;
        this.cartService = cartService;
        productsAPI = ProductsAPI.GetProductAPI();
    }




    [RelayCommand]
    private void GoBackHome() => _main.GoToHomeCommand.Execute(null);

    

    [RelayCommand] 
    private void GoToCart() => _main.NavigateToCart();
    
    [RelayCommand]
    private async Task SearchProduct(string productName)
    {
        if (!string.IsNullOrEmpty(productName))
        {
            List<ProductDisplay> product = await productsAPI.FindProducts(null, productName);
            if (product.Count > 0)
            {
                Console.WriteLine(product);
                _main.NavigateToProductCommand.Execute(product[0].sku); 
            }
            // Todo: else{} Popup/place a dialogue that says no items were found inside this else

            else if (product.Count == 0)
            {
                var dialogVm = new ItemsNotFoundDialogViewModel($"Item {productName} was not found");
                
                await _main.ShowDialogAsync();
            }
             
        }
        
    }
}