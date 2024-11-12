using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
    public class UserTable 
    {
        private readonly string _conString;

        public UserTable(IConfiguration configuration)
        {
            _conString = configuration.GetConnectionString("MyDatabaseConnection");
        }

        public int InsertUser(string name, string surname, string email)
        {
            string sql = "INSERT INTO UserTable (userName, userSurname, userEmail) VALUES (@userName, @userSurname, @userEmail)";
            using (SqlConnection con = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@userName", name);
                    cmd.Parameters.AddWithValue("@userSurname", surname);
                    cmd.Parameters.AddWithValue("@userEmail", email);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }
    }
}
