using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Creator : Alexander Maach
namespace OMS.DataTypes
{
    internal class User : IComparable<User>, IToString_Interface
    {

        private int user_ID;
        private string user_name;
        private Access_Level security_role;




        public User()
        {
            this.user_ID = -1;
            this.user_name = string.Empty;
            this.security_role = Access_Level.Customer;
        }

        public User(int user_ID, string user_name, Access_Level security_role)
        {
            this.user_ID = -1;
            this.user_name = string.Empty;
            this.security_role = Access_Level.Customer;
        }






        public int CompareTo(User? user_1)
        {
            if (user_1 == null) return 1;

            if ((this.user_ID - user_1.user_ID) != 0)
            {
                return this.user_ID - user_1.user_ID;
            }

            if (this.user_name.CompareTo(user_1.user_name) != 0)
            {
                return this.user_name.CompareTo(user_1.user_name);
            }

            if ((this.security_role - user_1.security_role) != 0)
            {
                return this.security_role - user_1.security_role;
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

            temp_string_1.Add( "User ID " + assign_string + " " + this.user_ID );
            temp_string_1.Add( "User name " + assign_string + " " + this.user_name );
            temp_string_1.Add( "Security role " + assign_string + " " + this.security_role.ToString() );

            stringList.AddRange(temp_string_1);

            return stringList;
        }

    }





    /// <summary>
    /// 
    /// </summary>
    public enum Access_Level
    {
        Admin,
        Super_User,
        Basic_User,
        Customer
    }


}
