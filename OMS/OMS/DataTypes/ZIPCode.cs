using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Creator : Alexander Maach
namespace OMS.DataTypes
{
    public class ZIPCode : IComparable<ZIPCode>, IToString_Interface
    {
        private int zip_ID;
        public int zip;
        public string country;
        public string city;

        public ZIPCode(ZIPCode zip_object)
        {
            this.zip_ID = zip_object.Get_ID();
            this.zip = zip_object.zip;
            this.country = zip_object.country;
            this.city = zip_object.city;
        }

        public ZIPCode(int zip, string country, string city)
        {
            this.zip_ID = -1;
            this.zip = zip;
            this.country = country;
            this.city = city;
        }

        public ZIPCode(int ID, int zip, string country, string city)
        {
            this.zip_ID = ID;
            this.zip = zip;
            this.country = country;
            this.city = city;
        }


        public int Get_ID()
        {
            return zip_ID;
        }



        public int CompareTo(ZIPCode? zip_1)
        {
            if (zip_1 == null) return 1;

            if ((this.zip - zip_1.zip) != 0)
            {
                return this.zip - zip_1.zip;
            }

            if (string.Compare(this.country, zip_1.country) != 0)
            {
                return string.Compare(this.country, zip_1.country);
            }

            if (string.Compare(this.city, zip_1.city) != 0)
            {
                return string.Compare(this.city, zip_1.city);
            }

            return 0;
        }


        public override string ToString()
        {
            return ToString_Custom(":", "\n");
        }


        public virtual string ToString_Custom(string assign_string = ":", string separator_string = "\n")
        {
            return String.Join(separator_string, ToString_List(assign_string));
        }


        public virtual List<string> ToString_List(string assign_string = ":")
        {
            List<string> temp_string_1 = new List<string>();

            List<string> stringList = new List<string>();

            temp_string_1.Add("ZIP " + assign_string + " " + this.zip);
            temp_string_1.Add("City " + assign_string + " " + this.city);
            temp_string_1.Add("Country " + assign_string + " " + this.country);

            stringList.AddRange(temp_string_1);

            return stringList;
        }

    }
}
