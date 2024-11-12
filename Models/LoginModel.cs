using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
    public class LoginModel
    {
        private readonly string? _con_string;

        public LoginModel(IConfiguration configuration)
        {
            _con_string = configuration.GetConnectionString("MyDatabaseConnection");
        }

        public int SelectUser(string email, string name)
        {
            int userID = -1;
            using (SqlConnection con = new SqlConnection(_con_string))
            {
                string sql = "SELECT userID FROM userTable WHERE userEmail = @Email AND userName = @Name";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Name", name);
                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    userID = Convert.ToInt32(result);
                }
            }
            return userID;
        }
    }
}