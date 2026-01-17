using System.Collections.Generic;
using PIM.Data.Queries;

namespace PIM.Access.Queries;

public class UpdateProductQuery : QueryBase
{
        public UpdateProductQuery(Dictionary<string, object?> parameters) : base(parameters)
        {
            base.query =
                "UPDATE products SET name = @name, manufacturer = @manufacturer, retail_price = @retailPrice, sales_price_vat = @salesPriceVAT, height = @height, width = @width, depth = @depth, weight = @weight, color = @color, long_descr = @longDescription WHERE sku = @sku";
        }
}