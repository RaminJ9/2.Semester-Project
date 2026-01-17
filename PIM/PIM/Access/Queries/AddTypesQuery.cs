using System.Collections.Generic;

namespace PIM.Data.Queries
{
    internal class AddTypesQuery : QueryBase {
        public AddTypesQuery(Dictionary<string, object?> parameters) : base(parameters) {
            base.query = "INSERT INTO types (product, category) VALUES (@product, @category);";
        }
    }
}
