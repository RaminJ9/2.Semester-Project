namespace PIM.Models;

public class PictureIdentifier
{
    public string name;
    public float priceInclVAT;
    public int[] size;
    public float weight;
    public string color;

    public PictureIdentifier( 
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