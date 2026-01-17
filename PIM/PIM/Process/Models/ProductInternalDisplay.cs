namespace PIM.Models;

public class ProductInternalDisplay
{
    public int Sku { get; set; }
    public string Name { get; set; }
    public float PriceInclVAT { get; set; }
    

    public ProductInternalDisplay(
        int sku,
        string name,
        float priceInclVAT

    )

    {
        Sku = sku;
        Name = name;
        PriceInclVAT = priceInclVAT;
    }

    public static explicit operator ProductInternalDisplay(ProductDisplay t)
    {
        return new ProductInternalDisplay
            (
            t.sku,
            t.name,
            t.priceInclVAT
            );
    }
}
