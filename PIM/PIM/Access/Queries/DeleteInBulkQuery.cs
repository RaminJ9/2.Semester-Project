using System.Collections.Generic;
using Npgsql;
using PIM.Data.Queries;
using PIM.Models;

namespace PIM.Access.Queries;

public class DeleteInBulkQuery : QueryBase
{
    public DeleteInBulkQuery(Dictionary<string, object?> parameters) : base(parameters)
    {
        base.query = "DELETE FROM products WHERE ";
    }

    public override object MapFromReader(NpgsqlDataReader reader)
    {
        int sku = reader.GetInt32(0);
        string name = reader.GetString(1);
        string manufacturer = reader.GetString(2);
        float priceInclVAT = reader.GetFloat(3);

        return new ProductMinimal(sku,
            name,
            priceInclVAT
        );
    }
    
    
}