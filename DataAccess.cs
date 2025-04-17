using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SalesProject_Backend.Models;

namespace SalesProject_Backend.DataAccess
{
    public class DataAccess
    {
        private readonly string _connectionString;

        // Constructor to get connection string from appsettings.json
        public DataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SalesDB");
        }

        // Method to get total revenue by product
        public List<ProductRevenue> GetRevenueByProduct(DateTime startDate, DateTime endDate)
        {
            List<ProductRevenue> revenueByProduct = new List<ProductRevenue>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT 
                        p.ProductName, 
                        SUM(od.QuantitySold * (p.UnitPrice - p.UnitPrice * p.Discount)) AS TotalRevenue
                    FROM SalesData sd
                    JOIN Products p ON sd.ProductID = p.ProductID
                    JOIN OrderDetails od ON sd.OrderID = od.OrderID
                    WHERE sd.OrderDate BETWEEN @startDate AND @endDate
                    GROUP BY p.ProductName
                    ORDER BY TotalRevenue DESC";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@startDate", startDate);
                command.Parameters.AddWithValue("@endDate", endDate);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    revenueByProduct.Add(new ProductRevenue
                    {
                        ProductName = reader.GetString(0),
                        TotalRevenue = reader.GetDecimal(1)
                    });
                }
            }

            return revenueByProduct;
        }

    }
}
