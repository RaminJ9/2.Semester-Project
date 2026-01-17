using PIM.Data.Queries;
using System.Collections.Generic;

namespace PIM.Data
{
    internal class AddProductsQuery : QueryBase {
        public AddProductsQuery (Dictionary<string, object?> parameters) : base(parameters) {
            base.query = "INSERT INTO products (name, manufacturer, retail_price, sales_price_vat, height, width, depth, weight, color, long_descr) " +
            "VALUES (@name, @manufacturer, @retail_price, @sales_price_vat, @height, @width, @depth, @weight, @color, @long_descr);";
        }
    }
}
