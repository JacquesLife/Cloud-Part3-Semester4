using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; 
using System;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TransactionController : Controller
    {
        private readonly string _connectionString;

        
        public TransactionController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyDatabaseConnection");
        }

       [HttpPost]
    public ActionResult PlaceOrder(int userID, int productID)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
        {
            string sql = "INSERT INTO transactionTable (userID, productID) VALUES (@UserID, @ProductID)";
            using (SqlCommand cmd = new SqlCommand(sql, con))

            {
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@ProductID", productID);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Redirect to the "work" page.
                    return RedirectToAction("Work", "Product", new { userID = userID });
                }
                else
                {
                    return NotFound(new { error = "Failed to place order." });
                }
            }
        }
    }
                catch (SqlException ex)
                {
                    return StatusCode(500, new { error = "An error occurred while processing the request." });
                }
                    catch (Exception)
                {
                    return StatusCode(500, new { error = "An unexpected error occurred." });
                }

            }
        }
    }