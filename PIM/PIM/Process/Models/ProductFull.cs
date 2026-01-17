using System;

namespace PIM.Models;
public  class ProductFull
{
    public int sku;
    public string name;
    public string manufacturer;
    public float retailPrice;
    public float priceInclVAT;
    public float priceExclVAT;
    public int[] size;
    public float weight;
    public string color;
    public string desc;
    public string shortDesc;
    public DateTime dateAdded;
    

    public ProductFull(int sku, 
                       string name, 
                       string manufacturer, 
                       float retailPrice, 
                       float priceInclVAT, 
                       float priceExclVAT, 
                       int[] size, 
                       float weight, 
                       string color, 
                       string desc, 
                       string shortDesc, 
                       DateTime dateAdded
                       )

    {
        this.sku = sku;
        this.name = name;
        this.manufacturer = manufacturer;
        this.retailPrice = retailPrice;
        this.priceInclVAT = priceInclVAT;
        this.priceExclVAT = priceExclVAT;
        this.size = size;
        this.weight = weight;
        this.color = color;
        this.desc = desc;
        this.shortDesc = shortDesc;
        this.dateAdded = dateAdded;
    }
}
