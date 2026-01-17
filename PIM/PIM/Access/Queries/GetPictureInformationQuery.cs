using System.Collections.Generic;
using Npgsql;
using PIM.Data.Queries;
using PIM.Models;

namespace PIM.Access.Queries;

internal class GetPictureInformationQuery : QueryBase
{
    public GetPictureInformationQuery(Dictionary<string, object?> parameters) : base(parameters)
    {
        base.query = "SELECT name, sales_price, height, width, depth, weight, color FROM products WHERE sku = @sku";
        //base.query = "SELECT * FROM FindProducts(@filterTerm, @searchTerm, @sortBy, @sortOrder);";
    }

    public override PictureIdentifier MapFromReader(NpgsqlDataReader reader)
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
        return new PictureIdentifier(name, salesPrice, size, weight, color);
    }
}