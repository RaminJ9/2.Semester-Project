using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.DataTypes
{
    /// <summary>
    /// <c> Address : </c> <br/>  
    /// A class that contains the informations related to an specific address. <br/>  
    /// </summary>
    public class Address
    {
        public string road;
        public int zip;
        public string country;
        public string city;


        public Address(string road, int zip, string country, string city)
        {
            this.road = road;
            this.zip = zip;
            this.country = country;
            this.city = city;
        }

        public Address(Address address)
        {
            road = address.road;
            zip = address.zip;
            country = address.country;
            city = address.city;
        }




        public static int Compare(Address address_1, Address address_2)
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


