using Npgsql;
using System.Collections.Generic;

namespace PIM.Data.Queries
{
    internal class GetLastSKUQuery : QueryBase {
        public GetLastSKUQuery(Dictionary<string, object?> parameters) : base(parameters) {
            base.query = "SELECT sku FROM products ORDER BY sku DESC LIMIT 1;";
        }
        public override object MapFromReader(NpgsqlDataReader reader) {
            int sku = reader.GetInt32(0);

            return sku;
        }
    }
}
