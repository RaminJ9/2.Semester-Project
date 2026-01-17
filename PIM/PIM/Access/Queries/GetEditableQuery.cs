using System.Collections.Generic;
using Npgsql;
using PIM.Models;

namespace PIM.Data.Queries;

internal class GetEditableQuery : QueryBase
{
    public GetEditableQuery(Dictionary<string, object?> parameters) : base(parameters)
    {
        base.query = "SELECT name, manufacturer, retail_price, sales_price_vat, sales_price, height, width, depth, weight, color, long_descr FROM products WHERE sku = @sku";
        //base.query = "SELECT * FROM FindProducts(@filterTerm, @searchTerm, @sortBy, @sortOrder);";
    }

    public override ProductEditable MapFromReader(NpgsqlDataReader reader)
    {
        var a = reader.GetColumnSchema();
        string name = reader.GetString(0);
        string manufacturer = reader.GetString(1);
        float retailPrice = reader.GetFloat(2);
        float salesPrice = reader.GetFloat(3);
        float priceExclVAT = reader.GetFloat(4);
        int[] size = new int[]
        {
            reader.GetInt32(5),
            reader.GetInt32(6),
            reader.GetInt32(7)
        };
        float weight = reader.GetFloat(8);
        string color = reader.GetString(9);
        string longDesc = reader.GetString(10);
        return new ProductEditable(name, manufacturer, retailPrice, salesPrice, priceExclVAT, size, weight, color, longDesc);
    }
}