using System.Collections.Generic;
using Npgsql;
using PIM.Data.Queries;

namespace SHOP.Database.Query

{
    internal class GetContentsQuery : QueryBase {
        
        public GetContentsQuery(Dictionary<string, object?> parameters) : base(parameters) {
            base.query = "SELECT * FROM ContentPages";
        }
        public override object MapFromReader(NpgsqlDataReader reader) {

            return (reader.GetString(1), reader.GetString(2));
        }
    }
}