using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using SHOP.Models;

namespace SHOP.Interfaces;

public interface ICartService : INotifyPropertyChanged
{
    int ItemCount { get; } // Read-only property for the count
    // Add other methods like AddItem(product), RemoveItem(product), ClearCart() etc.

    public void AddItem(ProductModel productModel, int quantity);

    public Task<Dictionary<ProductModel, int>> GetItemsList();

    public void RemoveItem(int productId);
    
    public Task<float> getTotal();

    public void ResetCart();


}