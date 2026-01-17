using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Creator : Alexander Maach
namespace OMS.DataTypes
{
    public class Shipping : IComparable<Shipping>, IToString_Interface
    {
        private int shipping_ID;
        public Shipping_Option shipping_option;
        public string? tracking_number;

        public Shipping(Shipping_Option shipping_option)
        {
            this.shipping_ID = -1;
            this.shipping_option = shipping_option;
            this.tracking_number = string.Empty;
        }


        public Shipping(int shipping_ID, Shipping_Option shipping_option, string tracking_number)
        {
            this.shipping_ID = shipping_ID;
            this.shipping_option = shipping_option;
            this.tracking_number = tracking_number;
        }


        public int Get_ID()
        {
            return shipping_ID;
        }




        public int CompareTo(Shipping? shipping_1)
        {
            if (shipping_1 == null) return 1;

            if ((this.shipping_ID - shipping_1.shipping_ID) != 0)
            {
                return this.shipping_ID - shipping_1.shipping_ID;
            }

            if ((this.shipping_option.CompareTo(shipping_1.shipping_option)) != 0)
            {
                return this.shipping_option.CompareTo(shipping_1.shipping_option);
            }

            if ((this.tracking_number != null) && (shipping_1.tracking_number != null))
            {
                if (tracking_number.CompareTo(shipping_1.tracking_number) != 0)
                {
                    return tracking_number.CompareTo(shipping_1.tracking_number);
                }
            }
            else if ((this.tracking_number == null) && (shipping_1.tracking_number != null))
            {
                return -1;
            }
            else if ((this.tracking_number != null) && (shipping_1.tracking_number == null))
            {
                return 1;
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

            List<string> temp_string_2 = shipping_option.ToString_List(assign_string);

            List<string> stringList = new List<string>();

            temp_string_1.Add( "Tracking number " + assign_string + " " + this.tracking_number );

            stringList.AddRange(temp_string_1);

            return stringList;
        }

    }




}
