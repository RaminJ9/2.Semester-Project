using System;
using System.Globalization;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PIM.Data;
using SHOP.Data;
using SHOP.Factories;
using SHOP.Interfaces;
using SHOP.Services;


namespace SHOP.ViewModels;

public partial class MainWindowViewModel: ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentPage;
    
    private PageFactory _pageFactory;

    public ICartService _cartService;
    
    public IShopDbService _shopDbService;

    
    public NavigationBarViewModel NavigationBarVm { get; } // Public property
    
  
    
    /*
     * the dialog stuff:
     */
    [ObservableProperty]
    private DialogViewModel _currentDialog = new ItemsNotFoundDialogViewModel { IsDialogOpen = false };

    public async Task ShowDialogAsync()
    {
        CurrentDialog = new ItemsNotFoundDialogViewModel();
        CurrentDialog.IsDialogOpen = true;
        await CurrentDialog.WaitAsync(); // Wait for dialog completion
        OnPropertyChanged(nameof(CurrentDialog));
    }

    public async Task ShowOrderDialogAsync(float total, string orderId)
    {
        CurrentDialog = new ItemsNotFoundDialogViewModel("The order " + orderId + " has been placed successfully! " + "Total: " + total.ToString(CultureInfo.InvariantCulture) );
        CurrentDialog.IsDialogOpen = true;
        await CurrentDialog.WaitAsync();
        OnPropertyChanged(nameof(CurrentDialog));
    }

    public MainWindowViewModel(PageFactory pageFactory, ICartService cartService, IShopDbService shopDbService)
    {
        _pageFactory= pageFactory;
        NavigationBarVm = new NavigationBarViewModel(this, cartService);
        var homeVm = new HomePageViewModel(this);
        CurrentPage = homeVm;
        _cartService = cartService;
        _shopDbService = shopDbService;
    }
    
    public async Task NavigateToCart() 
    {
        var vm = new CartViewModel(this, _cartService);
        await vm.LoadProductsAsync();        
        CurrentPage = vm;
        
    }
    
    [RelayCommand]
    public async Task NavigateToContent(string contentName) 
    {
        var vm  = new ContentPageViewModel(this, contentName, _shopDbService);
        await vm.LoadContentAsync();
        CurrentPage = vm;
    }
    
    [RelayCommand]
    public async Task NavigateToProduct(int productId) 
    {
        var vm  = new ProductPageViewModel(this, _cartService, productId);
        await vm.LoadProductAsync(productId);
        CurrentPage = vm;
    }
    
    [RelayCommand]
    public void GoToHome() => CurrentPage =_pageFactory.GetPageViewModel(ApplicationPageNames.Home);
    
    [RelayCommand]
    public async Task  NavigateToProducts(string category)
    {
        var vm  = new ProductsPageViewModel(this, category, _cartService);
        await vm.LoadProductsAsync();
        CurrentPage = vm;
    }
    
    
}