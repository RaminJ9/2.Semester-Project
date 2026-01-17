namespace PIM.Models;


internal class ProductPicture
{
    public string name;
    public float priceInclVAT;
    public int[] size;
    public float weight;
    public string color;

    public ProductPicture( 
        string name, 
        float priceInclVAT,
        int[] size, 
        float weight, 
        string color
    )

    {
        this.name = name;
        this.priceInclVAT = priceInclVAT;
        this.size = size;
        this.weight = weight;
        this.color = color;
    }
}