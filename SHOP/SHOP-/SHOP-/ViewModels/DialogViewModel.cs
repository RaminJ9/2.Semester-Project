using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SHOP.ViewModels;

public partial class DialogViewModel : ViewModelBase
{ 
    
    // this is a generic view model for dialogs.
    
    
    //Properties:
    
    // this will determine the dialog state whereas it is opened/closed.
    [ObservableProperty]
    private bool _isDialogOpen = false;
    
    
    protected TaskCompletionSource closeTask = new TaskCompletionSource();
    
    
    
    
    //constructor:
    
    public DialogViewModel()
    {
        
    }
    
    //Methods:

    public async Task WaitAsync()
    {
        await closeTask.Task;
    }

    public void ShowDialog()
    {
        if (closeTask.Task.IsCompleted)
            closeTask = new TaskCompletionSource();
        
        IsDialogOpen = true;
    }
    
    public void CloseDialog()
    {
        IsDialogOpen = false;
        
        closeTask.TrySetResult();
    }
}