using System.Collections.Generic;
using Npgsql;
using PIM.Models;

namespace PIM.Data.Queries;

internal class GetPictureInfoQuery : QueryBase
{
    public GetPictureInfoQuery(Dictionary<string, object?> parameters) : base(parameters)
    {
        base.query = "SELECT name,  sales_price_vat, height, width, depth, weight, color FROM products WHERE sku = @sku";
        //base.query = "SELECT * FROM FindProducts(@filterTerm, @searchTerm, @sortBy, @sortOrder);";
    }

    public override ProductPicture MapFromReader(NpgsqlDataReader reader)
    {
        var a = reader.GetColumnSchema();
        string name = reader.GetString(0);
        float salesPrice = reader.GetFloat(1);
        int[] size = new int[]
        {
            reader.GetInt32(2),
            reader.GetInt32(3),
            reader.GetInt32(4)
        };
        float weight = reader.GetFloat(5);
        string color = reader.GetString(6);
        return new ProductPicture(name, salesPrice, size, weight, color);
    }
}