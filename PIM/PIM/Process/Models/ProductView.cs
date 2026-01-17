using System;
using System.Collections.Generic;

namespace PIM.Process.Models
{
    internal class ProductView {
        public int sku;
        public string name;
        public string manufacturer;
        public float retail_price;
        public float sales_price_vat;
        public int[] size;
        public float weight;
        public string color;
        public string desc;
        public string categories;
        public ProductView(
                       string name,
                       string manufacturer,
                       float retail_price,
                       float sales_price_vat,
                       int[] size,
                       float weight,
                       string color,
                       string desc,
                       string categories
                       ) {
            this.name = name;
            this.manufacturer = manufacturer;
            this.retail_price = retail_price;
            this.sales_price_vat = sales_price_vat;
            this.size = size;
            this.weight = weight;
            this.color = color;
            this.desc = desc;
            this.categories = categories;
        }
        public List<object> ToList() {
            return new List<object> { name, manufacturer, retail_price, size[0], size[1], size[2], weight, color, desc, categories };
        }
    }
}
