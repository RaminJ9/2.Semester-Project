namespace PIM.Models;
public class ProductDisplay
{
    public int sku;
    public string name;
    public float priceInclVAT;
    public int[] size;
    public float weight;
    public string color;
    public string shortDesc;

    public ProductDisplay(int sku,
                       string name,
                       float priceInclVAT,
                       int[] size,
                       float weight,
                       string color,
                       string shortDesc
                       )

    {
        this.sku = sku;
        this.name = name;
        this.priceInclVAT = priceInclVAT;
        this.size = size;
        this.weight = weight;
        this.color = color;
        this.shortDesc = shortDesc;
    }
}
