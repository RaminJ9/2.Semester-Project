using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OMS.DataTypes
{


    /// <summary>
    /// <c> Addiliation : </c> <br/>  
    /// A class that contains the informations related to an affiliation like instituts, companies or groups. <br/>
    /// </summary>
    public class Affiliation
    {
        public int CVR;
        public string name;
        public Address address;

        public Affiliation(int CVR, string name, string road, int zip, string country, string city)
        {
            this.CVR = CVR;
            this.name = name;
            address = new Address(road, zip, country, city);
        }

        public Affiliation(int CVR, string name, Address address)
        {
            this.CVR = CVR;
            this.name = name;
            this.address = address;
        }




        public static int Compare(Affiliation affiliation_1, Affiliation affiliation_2)
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
