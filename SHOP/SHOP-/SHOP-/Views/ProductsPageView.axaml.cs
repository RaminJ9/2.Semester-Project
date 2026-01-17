using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using SHOP.Models;
using SHOP.ViewModels;

namespace SHOP.Views;

public partial class ProductsPageView : UserControl
{
    public ProductsPageView()
    {
        InitializeComponent();
    }
    
    // **** UPDATED EVENT HANDLER METHOD ****
    private void ProductsBorder_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        // 1. Get the Border that was clicked
        if (sender is Border clickedBorder)
        {
            // 2. Get the DataContext of the Border (ProductModel)
            if (clickedBorder.DataContext is ProductModel clickedProduct)
            {
                // 3. Get the ViewModel associated with this UserControl (ProductsPageViewModel)
                if (this.DataContext is ProductsPageViewModel productsViewModel)
                {
                    // 4. Get the Sku directly as a string
                    int? productSku = clickedProduct.SKU;

                    Console.WriteLine($"PointerPressed on product: {clickedProduct.Name} (Sku: {productSku})");

                    // 5. Call the command, passing the string Sku
                    if (productsViewModel.GoToProductPageCommand.CanExecute(productSku))
                    {
                        productsViewModel.GoToProductPageCommand.Execute(productSku);
                    }
                    else
                    {
                        Console.WriteLine($"Cannot execute GoToProductPageCommand for {clickedProduct.Name}");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Could not find ProductsPageViewModel from UserControl DataContext.");
                }
            }
            else
            {
                Console.WriteLine("Error: Could not get ProductModel from clicked Border DataContext.");
            }
        }
    }
    
    private void AddToCartButton_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        
        if (sender is Border clickedBorder)
        {
            // 2. Get the DataContext of the Border (ProductModel)
            if (clickedBorder.DataContext is ProductModel clickedProduct)
            {
                // 3. Get the ViewModel associated with this UserControl (ProductsPageViewModel)
                if (this.DataContext is ProductsPageViewModel productsViewModel)
                {
                    // 4. Get the Sku directly as a string
                    int? productSku = clickedProduct.SKU;

                    Console.WriteLine($"PointerPressed on product: {clickedProduct.Name} (Sku: {productSku})");

                    // 5. Call the command, passing the string Sku
                    
                    productsViewModel.AddItemToCartCommand.Execute(productSku);
                    
                    
                }
                else
                {
                    Console.WriteLine("Error: Could not find ProductsPageViewModel from UserControl DataContext.");
                }
            }
            else
            {
                Console.WriteLine("Error: Could not get ProductModel from clicked Border DataContext.");
            }
        }
        
    }
    // **** END OF UPDATED EVENT HANDLER METHOD ****
}
