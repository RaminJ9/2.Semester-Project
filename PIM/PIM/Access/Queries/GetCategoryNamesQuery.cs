
using System.Collections.Generic;
using Npgsql;

namespace PIM.Data.Queries {
    internal class GetCategoryNamesQuery : QueryBase {
        public GetCategoryNamesQuery(Dictionary<string, object?> parameters) : base(parameters) {
            base.query = "SELECT category FROM categories";
        }
        public override object MapFromReader(NpgsqlDataReader reader) {

            return (reader.GetString(0));
        }
    }
}