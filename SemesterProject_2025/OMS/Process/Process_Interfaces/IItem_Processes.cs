using OMS.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Process.Process_Interfaces
{
    internal interface IItem_Processes
    {





        // Get 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="item_id"></param>
        /// <returns></returns>
        public Item Get_OrderItem(int order_id, int item_id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public List<Item> Get_OrderItem_List(int order_id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="item_id"></param>
        /// <returns></returns>
        public double Get_OrderItem_PricePerUnit(int order_id, int item_id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="item_id"></param>
        /// <returns></returns>
        public int Get_OrderItem_Quantity(int order_id, int item_id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="item_id"></param>
        /// <returns></returns>
        public ItemStatus Get_OrderItem_Status(int order_id, int item_id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="item_id"></param>
        /// <returns></returns>
        public ReturnStatus Get_OrderItem_ReturnedStatus(int order_id, int item_id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="item_id"></param>
        /// <returns></returns>
        public int Get_OrderItem_ReturnedQuantity(int order_id, int item_id);





        // Update 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="item_id"></param>
        /// <param name="new_pricePerUnit"></param>
        /// <returns></returns>
        public bool Update_OrderItem_PricePerUnit(int order_id, int item_id, double new_pricePerUnit);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="item_id"></param>
        /// <param name="new_quantity"></param>
        /// <returns></returns>
        public bool Update_OrderItem_Quantity(int order_id, int item_id, int new_quantity);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="item_id"></param>
        /// <param name="new_status"></param>
        /// <returns></returns>
        public bool Update_OrderItem_Status(int order_id, int item_id, ItemStatus new_status);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="item_id"></param>
        /// <param name="new_returnedStatus"></param>
        /// <returns></returns>
        public bool Update_OrderItem_ReturnedStatus(int order_id, int item_id, ReturnStatus new_returnedStatus);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="item_id"></param>
        /// <param name="new_returnedQuantity"></param>
        /// <returns></returns>
        public bool Update_OrderItem_ReturnedQuantity(int order_id, int item_id, int new_returnedQuantity);






        // Delete 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="item_id"></param>
        /// <returns></returns>
        public bool Delete_OrderItem(int order_id, int item_id);








    }
}
