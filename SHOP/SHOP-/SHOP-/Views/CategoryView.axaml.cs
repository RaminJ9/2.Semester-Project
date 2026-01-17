using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using SHOP.ViewModels;

namespace SHOP.Views;

public partial class CategoryView : UserControl
{
    public CategoryView()
    {
        InitializeComponent();
    }
    // **** ADD THIS EVENT HANDLER METHOD ****
    private void CategoryBorder_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        // 1. Get the Border that was clicked (the sender)
        if (sender is Border clickedBorder)
        {
            // 2. Get the DataContext of the Border, which is the category name string
            if (clickedBorder.DataContext is string categoryName)
            {
                Console.WriteLine($"PointerPressed on category: {categoryName}"); // For debugging

                // 3. Get the ViewModel associated with this UserControl (CategoryViewModel)
                //    This ViewModel holds the reference to MainWindowViewModel needed for navigation.
                if (this.DataContext is CategoryViewModel categoryViewModel)
                {
                    // 4. Call the navigation logic (e.g., execute the command on the ViewModel)
                    //    Check if the command can execute before executing it
                    if (categoryViewModel.NavigateToProductsCommand.CanExecute(categoryName))
                    {
                        categoryViewModel.NavigateToProductsCommand.Execute(categoryName);
                    }
                    else
                    {
                        Console.WriteLine($"Cannot execute NavigateToCategoryCommand for {categoryName}");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Could not find CategoryViewModel from UserControl DataContext.");
                }
            }
            else
            {
                Console.WriteLine("Error: Could not get category name string from clicked Border DataContext.");
            }
        }
    }
    // **** END OF EVENT HANDLER METHOD ****
}