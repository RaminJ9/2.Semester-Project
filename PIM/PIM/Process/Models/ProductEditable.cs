namespace PIM.Models;

internal class ProductEditable
{
    public string name;
    public string manufacturer;
    public float retailPrice;
    public float priceInclVAT;
    public float priceExclVAT;
    public int[] size;
    public float weight;
    public string color;
    public string desc;

    public ProductEditable( 
        string name, 
        string manufacturer, 
        float retailPrice,
        float priceInclVAT,
        float priceExclVAT, 
        int[] size, 
        float weight, 
        string color, 
        string desc
    )

    {
        this.name = name;
        this.manufacturer = manufacturer;
        this.retailPrice = retailPrice;
        this.priceInclVAT = priceInclVAT;
        this.priceExclVAT = priceExclVAT;
        this.size = size;
        this.weight = weight;
        this.color = color;
        this.desc = desc;
    }
}