using System.Collections.Generic;
using Npgsql;
using PIM.Models;

namespace PIM.Data.Queries;

public class DeleteSingleQuery : QueryBase
{
    public DeleteSingleQuery(Dictionary<string, object?> parameters) : base(parameters)
    {
        base.query = "DELETE FROM types WHERE product = @id;DELETE FROM products WHERE sku = @id;";
    }

    public override object MapFromReader(NpgsqlDataReader reader)
    {
        int sku = reader.GetInt32(0);
        string name = reader.GetString(1);
        float priceInclVAT = reader.GetFloat(2);

        return new ProductMinimal(sku,
            name,
            priceInclVAT
        );
    }
}