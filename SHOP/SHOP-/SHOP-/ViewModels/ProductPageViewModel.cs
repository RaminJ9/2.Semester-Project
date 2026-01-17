using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PIM.Data;
using SHOP.Data;
using SHOP.Factories;
using SHOP.Interfaces;
using SHOP.Models;
using SHOP.Services;

namespace SHOP.ViewModels;

public partial class ProductPageViewModel() : PageViewModel(ApplicationPageNames.Product)
{
    // shows one product! (item details)
    private ProductsAPI productsAPI;
   
    private readonly MainWindowViewModel _main;
     
    private ICartService _cartService;
    
    [ObservableProperty] private int _quantity  = 1;
     

    [ObservableProperty]
    private ProductModel _product;

    public ProductPageViewModel(MainWindowViewModel main,ICartService cartService, int productId) : this()
    {
        _main = main;
        _cartService = cartService;
        productsAPI = ProductsAPI.GetProductAPI();
        
    }
    public async Task LoadProductAsync(int productId)
    {
     var product = await productsAPI.GetProduct(productId);
     
      Product = new ProductModel(product.sku, product.name,product.manufacturer,product.priceExclVAT, product.priceInclVAT, product.color,product.shortDesc, product.desc, product.size,
       product.weight);
      
    }
    
    [RelayCommand]
    public void AddItemToCart()
    {
        _cartService.AddItem(Product, Quantity);
    }

    
}