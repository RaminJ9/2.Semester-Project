using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
namespace SHOP.Views;

public partial class MainWindowView : Window
{
    private bool _searchVisible = false;
    
    public MainWindowView() 
    {
        InitializeComponent();
    }

    // Basket Button logic: (implement)
   
    
    // Toggle Search Button logic
    private void ToggleSearchVisibility(object? sender, RoutedEventArgs e)
    {
        _searchVisible = !_searchVisible;
        this.FindControl<TextBox>("SearchTextBox").IsVisible = _searchVisible;
    }

    
    
   
}
