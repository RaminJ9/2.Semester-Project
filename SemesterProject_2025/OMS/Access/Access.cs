using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.Process;
using OMS.DataTypes;

namespace OMS.Access
{
    public class Access
    {
        private static Access access_instance;
        private Order_Handler order_handler;
        private Customer_Handler customer_handler;
        private Stock_Handler stock_handler;
        private Login_Handler login_handler;

        private readonly string connString = "Host=localhost;Port=5432;Database=oms;Username=postgres;Password=1234";



        private Access ()
        {
            this.customer_handler = new Customer_Handler(connString);
            this.order_handler = new Order_Handler(connString, ref this.customer_handler);
            this.stock_handler = new Stock_Handler(connString);
            this.login_handler = new Login_Handler(connString);
        }


        public static Access GetInstance()
        {
            if (access_instance == null)
            {
                access_instance = new Access();
            }
            return access_instance;
        }


    }
}