using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Models
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }

    public class ProductTable
    {
        private readonly IConfiguration _configuration;

        public ProductTable()
        {
        }

        public ProductTable(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string Category { get; set; }
        public string Availability { get; set; }
        public string CustomerName { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string Country { get; set; }

        private OrderStatus ParseOrderStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
                return OrderStatus.Pending; // Default value

            if (Enum.TryParse<OrderStatus>(status, true, out OrderStatus result))
                return result;
            
            return OrderStatus.Pending; // Default if parse fails
        }

        public int InsertProduct(ProductTable product)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MyDatabaseConnection")))
                {
                    string sql = @"INSERT INTO productTable 
                                (productName, productPrice, productCategory, productAvailability, 
                                 customerName, orderStatus, country) 
                                VALUES 
                                (@ProductName, @Price, @Category, @Availability, 
                                 @CustomerName, @OrderStatus, @Country)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", product.ProductName ?? "");
                        cmd.Parameters.AddWithValue("@Price", product.Price ?? "");
                        cmd.Parameters.AddWithValue("@Category", product.Category ?? "");
                        cmd.Parameters.AddWithValue("@Availability", product.Availability ?? "");
                        cmd.Parameters.AddWithValue("@CustomerName", product.CustomerName ?? "");
                        cmd.Parameters.AddWithValue("@OrderStatus", product.OrderStatus.ToString());
                        cmd.Parameters.AddWithValue("@Country", product.Country ?? "");
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ProductTable> GetAllProducts()
        {
            List<ProductTable> products = new List<ProductTable>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MyDatabaseConnection")))
            {
                con.Open();
                string sql = "SELECT * FROM productTable";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductTable product = new ProductTable(_configuration)
                            {
                                ProductID = Convert.ToInt32(reader["productID"]),
                                ProductName = reader["productName"]?.ToString() ?? "",
                                Price = reader["productPrice"]?.ToString() ?? "",
                                Category = reader["productCategory"]?.ToString() ?? "",
                                Availability = reader["productAvailability"]?.ToString() ?? "",
                                CustomerName = reader["customerName"]?.ToString() ?? "",
                                OrderStatus = ParseOrderStatus(reader["orderStatus"]?.ToString()),
                                Country = reader["country"]?.ToString() ?? ""
                            };
                            products.Add(product);
                        }
                    }
                }
            }
            return products;
        }
    }
}