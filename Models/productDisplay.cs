using System.Data.SqlClient;
using WebApplication1.Models;

public class ProductDisplay
{
    private readonly string _con_string;

    public ProductDisplay(IConfiguration configuration)
    {
        _con_string = configuration.GetConnectionString("MyDatabaseConnection");
    }

    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public string ProductCategory { get; set; }
    public bool ProductAvailability { get; set; }
    public string CustomerName { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public string Country { get; set; }

    public ProductDisplay() { }

    public ProductDisplay(int id, string productName, decimal price, string category, 
                        bool availability, string customerName, OrderStatus orderStatus, string country)
    {
        ProductID = id;
        ProductName = productName;
        ProductPrice = price;
        ProductCategory = category;
        ProductAvailability = availability;
        CustomerName = customerName;
        OrderStatus = orderStatus;
        Country = country;
    }

    public List<ProductDisplay> SelectProducts()
    {
        List<ProductDisplay> products = new List<ProductDisplay>();

        using (SqlConnection con = new SqlConnection(_con_string))
        {
            string sql = @"SELECT productID, productName, productPrice, productCategory, 
                          productAvailability, customerName, orderStatus, country 
                          FROM productTable";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ProductDisplay product = new ProductDisplay
                {
                    ProductID = Convert.ToInt32(reader["productID"]),
                    ProductName = Convert.ToString(reader["productName"]),
                    ProductPrice = Convert.ToDecimal(reader["productPrice"]),
                    ProductCategory = Convert.ToString(reader["productCategory"]),
                    ProductAvailability = Convert.ToBoolean(reader["productAvailability"]),
                    CustomerName = Convert.ToString(reader["customerName"]),
                    OrderStatus = Enum.Parse<OrderStatus>(reader["orderStatus"].ToString()),
                    Country = Convert.ToString(reader["country"])
                };
                products.Add(product);
            }
            reader.Close();
        }
        return products;
    }
}