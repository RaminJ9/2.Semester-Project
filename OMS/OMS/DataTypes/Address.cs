using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Creator : Alexander Maach
namespace OMS.DataTypes
{
    /// <summary>
    /// <c> Address : </c> <br/>  
    /// A class that contains the informations related to an specific address. <br/>  
    /// </summary>
    public class Address : ZIPCode, IComparable<Address>, IToString_Interface
    {
        public string road;


        public Address(string road, ZIPCode zip_object) : base(zip_object)
        {
            this.road = road;
        }

        public Address(Address address) : base(address.Get_ID(), address.zip, address.country, address.city)
        {
            this.road = address.road;
        }




        public int CompareTo(Address? address_1)
        {
            if (address_1 == null) return 1;

            if ( base.CompareTo( (ZIPCode) address_1 ) != 0 )
            {
                return base.CompareTo((ZIPCode)address_1);
            }
            
            if (string.Compare(this.road, address_1.road) != 0)
            {
                return string.Compare(this.road, address_1.road);
            }
            
            return 0;
        }


        public override string ToString()
        {
            return ToString_Custom(":", "\n");
        }


        public override string ToString_Custom(string assign_string = ":", string separator_string = "\n")
        {
            return String.Join(separator_string, ToString_List(assign_string));
        }


        public override List<string> ToString_List(string assign_string = ":")
        {
            List<string> temp_string_1 = new List<string>();

            List<string> temp_string_2 = base.ToString_List(assign_string);

            List<string> stringList = new List<string>();

            temp_string_1.Add( "Road " + assign_string + " " + this.road );

            stringList.AddRange(temp_string_1);

            return stringList;
        }

    }







}


