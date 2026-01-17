using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Creator : Alexander Maach
namespace OMS.DataTypes
{
    public class Shipping_Option : IComparable<Shipping_Option>, IToString_Interface
    {
        private int shipping_option_ID;
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


        public int Get_ID()
        {
            return shipping_option_ID;
        }




        public int CompareTo(Shipping_Option? shipping_option_1)
        {
            if (shipping_option_1 == null) return 1;

            if ((this.shipping_option_ID - shipping_option_1.shipping_option_ID) != 0)
            {
                return this.shipping_option_ID - shipping_option_1.shipping_option_ID;
            }

            if ((this.delivery_option - shipping_option_1.delivery_option) != 0)
            {
                return this.delivery_option - shipping_option_1.delivery_option;
            }

            if ((this.shipping_price - shipping_option_1.shipping_price) != 0.0D)
            {
                return (int)(this.shipping_price - shipping_option_1.shipping_price);
            }

            if (this.company_name.CompareTo(shipping_option_1.company_name) != 0)
            {
                return this.company_name.CompareTo(shipping_option_1.company_name);
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

            temp_string_1.Add( "Shipping option " + assign_string + " " + this.shipping_option_ID );
            temp_string_1.Add( "Delivery option " + assign_string + " " + this.delivery_option.ToString() );
            temp_string_1.Add( "Shipping price " + assign_string + " " + this.shipping_price );
            temp_string_1.Add( "Company name " + assign_string + " " + this.company_name );

            stringList.AddRange(temp_string_1);

            return stringList;
        }

    }



    /// <summary>
    /// 
    /// </summary>
    public enum Delivery_option
    {
        Nearest_postbox,
        At_home
    }


}
    