using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Creator : Alexander Maach
namespace OMS.DataTypes
{
    public class Product : IComparable<Product>, IToString_Interface
    {
        private int product_ID;
        private string product_name;
        private int avaliable_quantity;
        private int stock_quantity;


        public Product(int product_ID, string product_name, int avalible_quantity, int stock_quantity)
        {
            this.product_ID = product_ID;
            this.product_name = product_name;
            this.avaliable_quantity = avalible_quantity;
            this.stock_quantity = stock_quantity;
        }




        public int Get_Product_ID()
        {
            return this.product_ID;
        }


        public string Get_Name()
        { 
            return this.product_name;
        }


        public int Get_Avaliable_Quantity()
        {
            return this.avaliable_quantity;
        }


        public int Get_Stock_Quantity()
        {
            return this.stock_quantity;
        }




        public string Set_Product_Name(string new_Product_Name)
        {
            this.product_name = new_Product_Name;
            return this.product_name;
        }


        public int Set_Avaliable_Quantity(int new_avaliable_quantity)
        {
            this.avaliable_quantity = new_avaliable_quantity;
            return this.avaliable_quantity;
        }


        public int Set_Stock_Quantity(int new_stock_quantity)
        {
            this.stock_quantity = new_stock_quantity;
            return this.stock_quantity;
        }









        public int CompareTo(Product? product_1)
        {
            if (product_1 == null) return 1;

            if ((this.product_ID - product_1.Get_Product_ID() ) != 0)
            {
                return this.product_ID - product_1.Get_Product_ID();
            }

            if (string.Compare(this.product_name, product_1.Get_Name()) != 0)
            {
                return string.Compare(this.product_name, product_1.Get_Name());
            }

            if ((this.avaliable_quantity - product_1.Get_Avaliable_Quantity()) != 0)
            {
                return this.avaliable_quantity - product_1.Get_Avaliable_Quantity();
            }

            if ((this.stock_quantity - product_1.Get_Stock_Quantity()) != 0)
            {
                return this.stock_quantity - product_1.Get_Stock_Quantity();
            }

            return 0;
        }


        public override string ToString()
        {
            return ToString_Custom(":", "\n");
        }


        public string ToString_Custom(string assign_string = ":", string separator_string = "\n")
        {
            return String.Join(separator_string, ToString_List(assign_string));
        }


        public List<string> ToString_List(string assign_string = ":")
        {
            List<string> temp_string_1 = new List<string>();

            List<string> stringList = new List<string>();

            temp_string_1.Add( "Product ID " + assign_string + " " + this.product_ID );
            temp_string_1.Add( "Product Name " + assign_string + " " + this.product_name );
            temp_string_1.Add( "Avalible " + assign_string + " " + this.avaliable_quantity );
            temp_string_1.Add( "In stock " + assign_string + " " + this.stock_quantity );

            stringList.AddRange(temp_string_1);

            return stringList;
        }

    }



}
