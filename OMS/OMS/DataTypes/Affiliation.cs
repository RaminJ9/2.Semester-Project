using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
// Creator : Alexander Maach
namespace OMS.DataTypes
{


    /// <summary>
    /// <c> Addiliation : </c> <br/>  
    /// A class that contains the informations related to an affiliation like instituts, companies or groups. <br/>
    /// </summary>
    public class Affiliation : IComparable<Affiliation>, IToString_Interface
    {
        private int affiliation_ID;
        public int CVR;
        public string name;
        public Address address;

        public Affiliation(Affiliation affiliation)
        {
            this.affiliation_ID = affiliation.Get_ID();
            this.CVR = affiliation.CVR;
            this.name = affiliation.name;
            this.address = affiliation.address;
        }

        public Affiliation(int CVR, string name, Address address)
        {
            this.affiliation_ID = -1;
            this.CVR = CVR;
            this.name = name;
            this.address = address;
        }

        public Affiliation(int affiliation_ID, int CVR, string name, Address address)
        {
            this.affiliation_ID = affiliation_ID;
            this.CVR = CVR;
            this.name = name;
            this.address = address;
        }


        public int Get_ID ()
        {
            return affiliation_ID;
        }




        public int CompareTo(Affiliation? affiliation_1)
        {
            if (affiliation_1 == null) return 1;

            if ((this.CVR - affiliation_1.CVR) != 0)
            {
                return this.CVR - affiliation_1.CVR;
            }

            if (string.Compare(this.name, affiliation_1.name) != 0)
            {
                return string.Compare(this.name, affiliation_1.name);
            }

            if (address.CompareTo(affiliation_1.address) != 0)
            {
                return address.CompareTo(affiliation_1.address);
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

            List<string> temp_string_2 = address.ToString_List(assign_string);

            List<string> stringList = new List<string>();

            temp_string_1.Add( "CVR " + assign_string + " " + this.CVR );
            temp_string_1.Add( "name " + assign_string + " " + this.name );

            stringList.AddRange(temp_string_1);
            stringList.AddRange(temp_string_2);

            return stringList;
        }

    }




}
