namespace PIM.Models;
public class ProductMinimal
{
    public int sku;
    public string name;
    public float priceInclVAT;

    public ProductMinimal(int sku,
        string name,
        float priceInclVAT
    )
    {
        this.sku = sku;
        this.name = name;
        this.priceInclVAT = priceInclVAT;
    }

    public override string ToString()
    {
        string info = "Name: " + this.name + "\n";
            info += "SKU: " + this.sku + "\n";
            info += "Price: " + this.priceInclVAT + "\n";
            
        return info;
    }
    
}
