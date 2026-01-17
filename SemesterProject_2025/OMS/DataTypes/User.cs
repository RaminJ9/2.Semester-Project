using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.DataTypes
{
    internal class User
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






        public static int Compare(User user_1, User user_2)
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



    public enum Access_Level
    {
        Admin,
        Super_User,
        Basic_User,
        Customer
    }




}
