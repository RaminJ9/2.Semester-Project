using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS;
using OMS.DataTypes;

namespace OMS.Access.Access_Interfaces
{
    internal interface ICustomer_Handler
    {


        /// <summary>
        /// Creates a new customer in the database.
        /// </summary>
        /// <param name="new_Customer">A <see cref="Customer"/> object containing the customer information to be inserted.</param>
        /// <returns>The unique ID of the newly created customer.</returns>
        public int Create_Customer_Database(Customer new_Customer);



        /// <summary>
        /// Fetches a customer from the database based on customer ID.
        /// </summary>
        /// <param name="customer_ID">The ID of the customer to retrieve.</param>
        /// <returns>A <see cref="Customer"/> object if found; otherwise, <c>null</c>.</returns>
        public Customer? Fetch_Customer_Database(int customer_ID);



        /// <summary>
        /// Updates an existing customer's information in the database.
        /// </summary>
        /// <param name="customer_ID">The ID of the customer to update.</param>
        /// <param name="updated_Customer">A <see cref="Customer"/> object containing the updated customer information.</param>
        /// <returns>The number of records affected (should be 1 if successful).</returns>
        public int Update_Customer_Database(int customer_ID, Customer updated_Customer);



        /// <summary>
        /// Deletes a customer from the database.
        /// </summary>
        /// <param name="customer_ID">The ID of the customer to delete.</param>
        /// <returns>The number of records affected (should be 1 if successful).</returns>
        public int Delete_Customer_Database(int customer_ID);







        // Search

        /// <summary>
        /// Searches for customers who have placed an order with the specified order ID.
        /// </summary>
        /// <param name="order_ID">The order ID to search by.</param>
        /// <returns>A list of <see cref="Customer"/> objects that match the criteria, or <c>null</c> if none found.</returns>
        public List<Customer>? Search_forCustomer_OrderID(int order_ID);


        /// <summary>
        /// Searches for customers who have ordered a specific product.
        /// </summary>
        /// <param name="product_ID">The ID of the product to search by.</param>
        /// <returns>A list of <see cref="Customer"/> objects that have purchased the specified product, or <c>null</c> if none found.</returns>
        public List<Customer>? Search_forCustomer_ProductID(int product_ID);


        /// <summary>
        /// Searches for customers by their email address.
        /// </summary>
        /// <param name="email">The email address to search by.</param>
        /// <returns>A list of <see cref="Customer"/> objects with the given email, or <c>null</c> if none found.</returns>
        public List<Customer>? Search_forCustomer_Email(string email);


        /// <summary>
        /// Searches for customers by their full name.
        /// </summary>
        /// <param name="first_Name">The first name of the customer.</param>
        /// <param name="last_Name">The last name of the customer.</param>
        /// <returns>A list of <see cref="Customer"/> objects that match the given first and last names, or <c>null</c> if none found.</returns>
        public List<Customer>? Search_forCustomer_Name(string first_Name, string last_Name);










        





    }
}
