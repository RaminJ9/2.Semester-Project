using Npgsql;
using PIM.Models;
using System;
using System.Collections.Generic;

namespace PIM.Data.Queries
{
    internal class FindProductsQuery : QueryBase
    {
        public FindProductsQuery(Dictionary<string, object?> parameters) : base(parameters)
        {
            //base.query = "SELECT * FROM products;";
            base.query = "SELECT * FROM FindProducts(@filterTerm, @searchTerm);";
        }
        

        public override ProductDisplay MapFromReader(NpgsqlDataReader reader)
        {
            int sku = reader.GetInt32(0);
            string name = reader.GetString(1);
            float priceInclVAT = reader.GetFloat(2);
            int[] size = new int[]
            {
                reader.GetInt32(3),
                reader.GetInt32(4),
                reader.GetInt32(5)
            };
            float weight = reader.GetFloat(6);
            string color = reader.GetString(7);
            string shortDesc = reader.GetString(8);

            return new ProductDisplay(sku,
                                   name,
                                   priceInclVAT,
                                   size,
                                   weight,
                                   color,
                                   shortDesc
                                   );
        }
    }

    internal class FindProductQuery : QueryBase
    {
        public FindProductQuery(Dictionary<string, object?> parameters) : base(parameters)
        {
            //base.query = "SELECT * FROM products;";
            base.query = "SELECT * FROM products Where sku = @sku";
        }
        
        public override ProductFull MapFromReader(NpgsqlDataReader reader)
        {
            int sku = reader.GetInt32(0);
            string name = reader.GetString(1);
            string manufacturer = reader.GetString(2);
            float retailPrice = reader.GetFloat(3);
            float priceInclVAT = reader.GetFloat(4);
            float priceExclVAT = reader.GetFloat(5);
            int[] size = new int[]
            {
                reader.GetInt32(6),
                reader.GetInt32(7),
                reader.GetInt32(8)
            };
            float weight = reader.GetFloat(9);
            string color = reader.GetString(10);
            string desc = reader.GetString(11);
            string shortDesc = reader.GetString(12);
            DateTime dateAdded = reader.GetDateTime(13);
            

            return new ProductFull( sku, name, manufacturer, retailPrice, priceInclVAT, priceExclVAT,
                 size, weight, color, desc, shortDesc, dateAdded
            );
        }
    }
    
    
}
