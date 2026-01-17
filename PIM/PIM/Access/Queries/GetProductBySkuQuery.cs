using Npgsql;
using PIM.Models;
using System;
using System.Collections.Generic;

namespace PIM.Data.Queries
{
    internal class GetProductBySkuQuery : QueryBase
    {
        public GetProductBySkuQuery(Dictionary<string, object?> parameters) : base(parameters)
        {
            base.query = "SELECT * FROM products WHERE sku = @id;";
            //base.query = "SELECT * FROM FindProducts(@filterTerm, @searchTerm, @sortBy, @sortOrder);";
        }

        public override ProductFull MapFromReader(NpgsqlDataReader reader)
        {
            var a = reader.GetColumnSchema();
            int sku = reader.GetInt32(0);
            string name = reader.GetString(1);
            string manufacturer = reader.GetString(2);
            float retailPrice = reader.GetFloat(3);
            float salesPrice = reader.GetFloat(4);
            float priceExclVAT = reader.GetFloat(5);
            int[] size = new int[]
            {
                reader.GetInt32(6),
                reader.GetInt32(7),
                reader.GetInt32(8)
            };
            float weight = reader.GetFloat(9);
            string color = reader.GetString(10);
            string longDesc = reader.GetString(11);
            string shortDesc = reader.GetString(12);
            DateTime date = reader.GetDateTime(13);
            return new ProductFull(sku, name, manufacturer, retailPrice, salesPrice, priceExclVAT, size, weight, color, longDesc, shortDesc, date);
        }
    }
}
