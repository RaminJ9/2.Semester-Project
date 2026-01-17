using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SHOP.ViewModels;

public partial class OrderConfirmationDialogViewModel : DialogViewModel
{
    [ObservableProperty] private string _orderId;

    [ObservableProperty] private ObservableCollection<string> _productSummary;

    [ObservableProperty] private decimal _subTotal;
    
    [ObservableProperty] private string _message = "Your order has been placed successfully!";
    
    [ObservableProperty] private bool _confirmed;
    
    public OrderConfirmationDialogViewModel(string? customMessage = null)
    {
        if(!string.IsNullOrEmpty(customMessage))
            Message = customMessage;
        
        // Mock Id generating:
        OrderId = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

        ProductSummary = new ObservableCollection<string>();
    }

   
   

    [RelayCommand]
    public void Confirm()
    {
        Confirmed = true;
        CloseDialog();
    }

    [RelayCommand]
    private void Cancel()
    {
        Confirmed = false;
        CloseDialog();
    }
}