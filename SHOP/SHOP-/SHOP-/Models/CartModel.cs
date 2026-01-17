using System.Collections.Generic;
using System.Linq;

namespace SHOP.Models;

/// <summary>
/// Represents the user's shopping cart, containing a collection of CartItems.
/// This class focuses on holding the state (the items).
/// Calculation logic (like Subtotal, Total) is often better placed in a CartService
/// to keep the model clean and logic centralized.
/// </summary>

public class CartModel
{
  
    
    
   // private int _cartItemCount = 0;
    // The Model that represents the shopping Cart itself

    public Dictionary<int, int> Items { get; set; } = new Dictionary<int, int>();
    
    // --- Methods related to querying the Cart state ---

    /// <summary>
    /// Gets the total number of individual product units in the cart (sum of quantities).
    /// </summary>
    /// <returns>Total number of units.</returns>
    public int GetTotalUnitCount()
    {
        int count = 0;
        
        foreach (var item in Items)
        {
            count += item.Value;
             
        }
        return  count;
    }

    /// <summary>
    /// Gets the number of distinct line items in the cart.
    /// </summary>
    /// <returns>Number of distinct product types.</returns>
   
    public int GetLineItemCount() => Items.Count;

   

    public void resetCart()
    {
        Items = new Dictionary<int, int>();
    }
    

    
    // --- Constructor ---
    public CartModel()
    {
        // Initialization, if any, happens via the Items property initializer.
        Items = new Dictionary<int, int>();
    }

    // Note: Methods to AddItem, RemoveItem, UpdateQuantity are usually placed
    // in the CartService that *manages* this Cart instance, rather than directly
    // on the Cart model itself. This keeps the model focused on data representation.
    
    /*
    public bool IsCartBadgeVisible => CartItemCount > 0;
    // OR if not using source generator for CartItemCount:
    // public bool IsCartBadgeVisible { get { return _cartItemCount > 0; } }

    //todo: might need a cotr. 
    
    
    //todo: move to service later
    // In a real app, this would be triggered by user actions
    public void AddItemToCart()
    {
        CartItemCount++; // Just increment the count; the property setter notifies the UI
        // In reality, you'd update your underlying cart data model here too
    }
    // Example method to simulate removing items
    public void RemoveItemFromCart()
    {
        if (CartItemCount > 0)
        {
            CartItemCount--;
        }
        // Update underlying cart data model here
    }
    
    // Example method to populate for testing
    private void SimulateAddingItems()
    {
        CartItemCount = 3;
    }
    */
}
