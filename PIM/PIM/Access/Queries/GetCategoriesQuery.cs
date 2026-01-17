using Npgsql;
using System.Collections.Generic;

namespace PIM.Data.Queries
{
    internal class GetCategoriesQuery : QueryBase {
        public GetCategoriesQuery(Dictionary<string, object?> parameters) : base(parameters) {
            base.query = "SELECT * FROM categories";
        }
        public override object MapFromReader(NpgsqlDataReader reader) {

            return (reader.GetString(1), reader.GetInt32(0));
        }
    }
}
