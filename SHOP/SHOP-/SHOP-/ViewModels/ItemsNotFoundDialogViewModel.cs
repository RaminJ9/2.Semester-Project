using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SHOP.ViewModels;

public partial class ItemsNotFoundDialogViewModel : DialogViewModel
{
    [ObservableProperty] private string _message = "The Item you searched for is not found";
    

    [ObservableProperty] private bool _confirmed;

    
    //Todo: check if this constructor is needed:
    // The Constructor allows a custom message 

    public ItemsNotFoundDialogViewModel(string? customMessage = null) : base()
    {
        if (!string.IsNullOrEmpty(customMessage))
            Message = customMessage; // ("The Item you searched for is not found")
    }
    
    
    [RelayCommand]
    public void ItemFound()
    {
        Confirmed = true;
        CloseDialog();
    }

    [RelayCommand]
    public void ItemNotFound()
    {
        Confirmed = false;
        CloseDialog();
    }
}