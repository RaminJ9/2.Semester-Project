using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.DataTypes
{
    public class Shipping_Option
    {
        public int shipping_option_ID;
        public Delivery_option delivery_option;
        public double shipping_price;
        public string company_name;


        public Shipping_Option(Delivery_option delivery_option, double shipping_price, string company_name)
        {
            shipping_option_ID = -1;
            this.delivery_option = delivery_option;
            this.shipping_price = shipping_price;
            this.company_name = company_name;
        }

        public Shipping_Option(int shipping_option_ID, Delivery_option delivery_option, double shipping_price, string company_name)
        {
            this.shipping_option_ID = shipping_option_ID;
            this.delivery_option = delivery_option;
            this.shipping_price = shipping_price;
            this.company_name = company_name;
        }




        public static int Compare(Shipping_Option shipping_option_1, Shipping_Option shipping_option_2)
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




    public enum Delivery_option
    {
        Nearest_postbox,
        At_home
    }


}
