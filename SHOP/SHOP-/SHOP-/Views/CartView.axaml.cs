using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using SHOP.Models;
using SHOP.ViewModels;

namespace SHOP.Views;

public partial class CartView : UserControl
{
    public CartView()
    {
        InitializeComponent();
    }
    
    private void RemoveFromCartButton_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        
        if (sender is Border clickedBorder)
        {
            // 2. Get the DataContext of the Border (ProductModel)
            if (clickedBorder.DataContext is KeyValuePair<ProductModel, int> clickedProduct)
            {
                // 3. Get the ViewModel associated with this UserControl (ProductsPageViewModel)
                if (this.DataContext is CartViewModel cartViewModel)
                {
                    // 4. Get the Sku directly as a string
                    int? productSku = clickedProduct.Key.SKU;

                    Console.WriteLine($"PointerPressed on product: {clickedProduct.Key.Name} (Sku: {productSku})");

                    // 5. Call the command, passing the string Sku
                    
                    cartViewModel.RemoveItemCommand.Execute(productSku);
                    
                    
                }
                else
                {
                    Console.WriteLine("Error: Could not find CartViewModel from UserControl DataContext.");
                }
            }
            else
            {
                Console.WriteLine("Error: Could not get ProductModel from clicked Border DataContext.");
            }
        }
        
    }
}