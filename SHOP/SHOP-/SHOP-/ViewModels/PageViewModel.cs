using CommunityToolkit.Mvvm.ComponentModel;
using SHOP.Data;

namespace SHOP.ViewModels;

public partial class PageViewModel(ApplicationPageNames pageName): ViewModelBase
{
    
    [ObservableProperty]
    private ApplicationPageNames _pageName = pageName;
}