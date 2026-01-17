using System;
using System.Collections.Generic;

namespace SHOP.Models;

public class CategoryModel
{
    public string Id { get; set; } // Unique ID (e.g., "laptops", "gaming-laptops")
    public string Name { get; set; } // Display Name (e.g., "Laptops", "Gaming Laptops")
   
   

    public List<string> Categories { get; set; } = new List<string>();
    
    // List to hold child categories
    
    // Might be populated lazily by a service when needed
    public List<string> SubCategories { get; set; } = new List<string>();

    
    

    // Constructor examples
    public CategoryModel(string name)
    {
        Name = name;
    }

    public CategoryModel()
    {
        Console.WriteLine("It is working!");
    } 
    
    
    public List<string> GetSampleCategories()
    {
        List<string> L = new List<string>();
        L.Add("Desktop");
        L.Add("Mouse");
        L.Add("Laptop");
        L.Add("Gaming");
        L.Add("School");
        L.Add("Monitor");
        L.Add("Component");
        L.Add("Graphics card");
        L.Add("Processor");
        L.Add("RAM");
        L.Add("Motherboard");
        L.Add("Cases");
        return L;
    }
}



