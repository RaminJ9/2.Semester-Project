using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.Access.Access_Interfaces;
using OMS.DataTypes;
// Creator : Alexander Maach
namespace OMS.Access
{
    public class Login_Handler : ILogin_Handler
    {
        
        private readonly string connString;




        public Login_Handler(string connString)
        {
            this.connString = connString;
        }


        // Notes: 
        // Protect against SQL Injection. 
        // Check input.

    }
}
