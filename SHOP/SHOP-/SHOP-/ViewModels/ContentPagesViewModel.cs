using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SHOP.Data;
using SHOP.Models;

namespace SHOP.ViewModels;

public partial class ContentPagesViewModel() : PageViewModel(ApplicationPageNames.ContentPage)
{
    
    private readonly MainWindowViewModel _mainViewModel; // Or a dedicated INavigationService

    private ContentModel _contentModel;

    [ObservableProperty]
    private ObservableCollection<string> _contents;
    public string Title { get; set; } = "How to Replace Your CPU";
    public string Subtitle { get; set; } = "By ARNE Hardware Team - May 2025";

    public string Body { get; set; } = """
                                       Replacing your CPU might sound intimidating, but with the right steps, it's actually manageable...

                                       1. Power down and unplug your PC.
                                       2. Open the case and locate your CPU socket.
                                       3. Remove the cooling system.
                                       4. Unlock the CPU socket.
                                       5. Carefully remove the old CPU.
                                       6. Insert the new CPU in the correct orientation.
                                       7. Apply thermal paste.
                                       8. Reattach the cooler.
                                       9. Close your case and power on the system.

                                       Always double-check your motherboard's CPU compatibility before starting.
                                       """;


    // Constructor
    public ContentPagesViewModel(MainWindowViewModel mainViewModel) : this()
    {

        _mainViewModel = mainViewModel;
        _contentModel = new ContentModel();
        Contents =
            new ObservableCollection<string>(_contentModel.GetSampleContents());
    }
    
    [RelayCommand]
    public void NavigateToContentPage(string contentName)
    {
        _mainViewModel.NavigateToContentCommand.Execute(contentName);

    }
}