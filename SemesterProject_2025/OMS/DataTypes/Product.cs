using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.DataTypes
{
    public class Product
    {

        private int product_ID;
        private string product_name;
        private int avalible_quantity;
        private int stock_quantity;


        public Product() 
        {
            this.product_ID = -1;
            this.product_name = string.Empty;
            this.avalible_quantity = 0;
            this.stock_quantity = 0;
        }

        public Product(int product_ID, string product_name, int avalible_quantity, int stock_quantity)
        {
            this.product_ID = product_ID;
            this.product_name = product_name;
            this.avalible_quantity = avalible_quantity;
            this.stock_quantity = stock_quantity;
        }



        public static int Compare(Product product_1, Product product_2)
        {
            return -1;
        }


        public override string ToString()
        {
            return ToString_Custom(":", "\n");
        }


        public string ToString_Custom(string assign_string = ":", string separator_string = "\n")
        {
            return String.Join(separator_string, ToString_Array(assign_string));
        }


        public string[] ToString_Array(string assign_string = ":")
        {
            string[] stringArray = new string[1];
            stringArray[0] = "";
            return stringArray;
        }



    }
}
