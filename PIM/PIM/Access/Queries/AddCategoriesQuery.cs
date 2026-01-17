using PIM.Data.Queries;
using System.Collections.Generic;

namespace PIM.Data
{
    internal class AddCategoriesQuery : QueryBase {
        public AddCategoriesQuery(Dictionary<string, object?> parameters) : base(parameters) {
            base.query = "INSERT INTO categories (category) VALUES (@category);";
        }
    }
}
