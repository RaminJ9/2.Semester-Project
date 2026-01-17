using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SHOP.Data;
using SHOP.Factories;
using SHOP.Models;
using PIM.Data;
using SHOP.Interfaces;

namespace SHOP.ViewModels;

public partial class ProductsPageViewModel() : PageViewModel(ApplicationPageNames.Products)
{
  //  private ProductsModel _productsModel;
   
    private ProductsAPI productsAPI;
        

    [ObservableProperty] private ObservableCollection<ProductModel> _productslist;
    
    private PageFactory _pageFactory;
    private static readonly SemaphoreSlim _productLock = new SemaphoreSlim(1, 1);

    
    private readonly MainWindowViewModel _main;
    public string Category { get;}
    
    public ICartService CartService { get; }


    public ProductsPageViewModel(MainWindowViewModel main, string category, ICartService cartService) : this()
    {
        _main = main;
        Category = category;
        CartService = cartService;
        productsAPI = ProductsAPI.GetProductAPI();
    }
    
    [RelayCommand]
    public void GoBackHome()
    {
        _main.CurrentPage = new HomePageViewModel();  // this
    }
    
    [RelayCommand]
    public void GoToProductPage(int productId)
    {
        _main.NavigateToProductCommand.Execute(productId);
    }
    
    public async Task LoadProductsAsync()
    {
        var products = await productsAPI.FindProducts(Category);

        _productslist = new ObservableCollection<ProductModel>();

        ProductModel pm;
        
        foreach (var product in products)
        {
             pm = new ProductModel(product.sku, product.name, product.priceInclVAT, product.size,
                product.weight, product.color, product.shortDesc);
             
             _productslist.Add(pm);
             
        }
       
    }
    
    [RelayCommand]
    public async Task AddItemToCart(int sku)
    {
        //await AddItemToCartTask(sku);
    }
    
    private async Task AddItemToCartTask(int sku)
    {
        
        var product = await productsAPI.GetProduct(sku);
         
        ProductModel productM = new ProductModel(product.sku, product.name,product.manufacturer,product.priceExclVAT, product.priceInclVAT, product.color,product.shortDesc, product.desc, product.size,
            product.weight);

        await Dispatcher.UIThread.InvokeAsync(() => // PROBLEM IS HERE Cuz Avalonia is not thread safe!
        {
            CartService.AddItem(productM, 1);
        });
            

        
        
    }
    
    
    
    
    
}