using Npgsql;
using PIM.Data.Queries;
using System.Collections.Generic;

namespace PIM.Access.Queries
{
    internal class GetAllSkuQuery : QueryBase
    {
        public GetAllSkuQuery(Dictionary<string, object?> parameters) : base(parameters)
        {
            base.query = "SELECT sku FROM products;";
        }

        public override object MapFromReader(NpgsqlDataReader reader)
        {
            return reader.GetInt32(0);
        }
    }
}
