using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.DataTypes
{
    public class Shipping
    {
        public int shipping_ID;
        public Shipping_Option shipping_option;
        public string? tracking_number;

        public Shipping(Shipping_Option shipping_option)
        {
            shipping_ID = -1;
            this.shipping_option = shipping_option;
            this.tracking_number = string.Empty;
        }


        public Shipping(int shipping_ID, Shipping_Option shipping_option, string tracking_number)
        {
            this.shipping_ID = shipping_ID;
            this.shipping_option = shipping_option;
            this.tracking_number = tracking_number;
        }




        public static int Compare(Shipping shipping_1, Shipping shipping_2)
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
