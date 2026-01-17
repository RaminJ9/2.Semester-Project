using Npgsql;
using PIM.Data.Queries;
using PIM.Process.Models;
using System;
using System.Collections.Generic;

namespace PIM.Access.Queries
{
    internal class GetAllProductInformationQuery : QueryBase {
        public GetAllProductInformationQuery(Dictionary<string, object?> parameters) : base(parameters) {
            base.query = "select * FROM exportSearch(@categoryTerm,@searchTerm);";
        }
        public override ProductView MapFromReader(NpgsqlDataReader reader) {
            var a = reader.GetColumnSchema();
            string name = reader.GetString(0);
            string manufacturer = reader.GetString(1);
            float retail_price = reader.GetFloat(2);
            float sales_price_vat = reader.GetFloat(3);
            int[] size = new int[]
            {
                reader.GetInt32(4),
                reader.GetInt32(5),
                reader.GetInt32(6)
            };
            float weight = reader.GetFloat(7);
            string color = reader.GetString(8);
            string longDesc = reader.GetString(9);
            string categories = reader.GetString(10);
            return new ProductView(name, manufacturer, retail_price, sales_price_vat, size, weight, color, longDesc, categories);
        }
    }
}
