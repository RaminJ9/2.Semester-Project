using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SHOP.Models;

public class ProductModel
{
    // properties of the product:
    
    // fetch product's details from PIM:
    public int SKU { get; set; }
    public string Name { get; set;}
    public string Manufacturer { get; set; }
    public float RetailPrice { get; set; }
    
    public float SalesPrice { get; set; } 
    public string Color { get; set; }
    
    //public Bitmap? ImageUrl { get; set; }
    
    //fetch the product's description from PIM:
    public string ShortDescription { get; set; }
    public string LongDescription { get; set; }

    // fetch the product's dimensions from PIM:
    public int Height { get; set; }
    public int Width { get; set; }
    public int Depth { get; set; }
    public float Weight { get; set; }
    
    //Reading the images from a local assets folder is made during to some difficulties and technical issues from the DAM's site! Unfortunately!
    public static Bitmap GetImagePath(string imageNameWithoutExtension)
    {
        var uri = $"avares://SHOP/Assets/Media/{imageNameWithoutExtension}.png";
        return new Avalonia.Media.Imaging.Bitmap(uri);;
    }
    

    // the constructor that fetches the full product's details from PIM:
    public ProductModel(int sku, string name, string manufacturer, float retailPrice, float salesPrice, string color, string shortDescription, string longDescription, int[] size, float weight)
    {
        this.SKU = sku;
        this.Name = name;
        this.Manufacturer = manufacturer;
        this.RetailPrice = retailPrice;
        this.SalesPrice = salesPrice ;
        this.Color = color;
        this.ShortDescription = shortDescription;
        this.LongDescription = longDescription;
        this.Height = size[0];
        this.Width = size[1];
        this.Depth = size[2];
        this.Weight = weight;
        //ImageUrl = GetImagePath(Name);
    }
    
    
    // fetch the Display product details from PIM:
    public ProductModel(
        int sku,
        string name,
        float priceInclVAT,
        int[] size,
        float weight,
        string color,
        string shortDesc
    )
    {
        this.SKU = sku;
        this.Name = name;
        this.SalesPrice = priceInclVAT;
        this.Height = size[0];
        this.Width = size[1];
        this.Depth = size[2];
        this.Weight = weight;
        this.Color = color;
        this.ShortDescription = shortDesc;
       // ImageUrl = GetImagePath(Name.Replace("","_"));
        
    }
}