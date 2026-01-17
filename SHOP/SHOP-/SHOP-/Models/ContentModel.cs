using System;
using System.Collections.Generic;

namespace SHOP.Models;

public class ContentModel
{
    public string Content { get; set; } 
    public string Name { get; set; } 
   
   

    public List<string> Contents { get; set; } = new List<string>();


    // Constructor examples
    public ContentModel(string name, string content)
    {
        Name = name;
        Content = content;     
    }

    public ContentModel()
    {
        Console.WriteLine("It is working!");
    } 
    
    
    public List<string> GetSampleContents()
    {
        List<string> L = new List<string>();
        L.Add("How to Replace Your CPU");
        L.Add("Best CPUs for Gaming in 2025");
        L.Add("Understanding CPU Cores and Threads");
        L.Add("How to Choose the Right Laptop for School");
        L.Add("Best Laptops for Creative Work");
        L.Add("Ultrabook vs. Gaming Laptop: What\\'s the Difference?");
        L.Add("How to Clean Your Laptop Safely");
        L.Add("Common Laptop Issues and How to Fix Them");
        L.Add("What is Refresh Rate and Why Does It Matter?");

        return L;
    }
}