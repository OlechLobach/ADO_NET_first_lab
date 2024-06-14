using System;
using System.Data;
using System.Data.SqlClient;

namespace ADO_NET_first_lab
{
    public static class DBManager
    {
        private static string connectionString = @"Data Source=YourServerName;Initial Catalog=VegetablesAndFruits;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public static void CreateProductsTable()
        {
            using (SqlConnection connection = GetConnection())
            {
                string createTableQuery = @"
                    CREATE TABLE Products (
                        Id INT PRIMARY KEY IDENTITY,
                        Name NVARCHAR(100) NOT NULL,
                        Type NVARCHAR(50) NOT NULL,
                        Color NVARCHAR(50) NOT NULL,
                        Calories INT NOT NULL
                    )";

                SqlCommand command = new SqlCommand(createTableQuery, connection);
                command.ExecuteNonQuery();
                Console.WriteLine("Table 'Products' created.");
            }
        }

        public static void InsertTestData()
        {
            using (SqlConnection connection = GetConnection())
            {
                string insertDataQuery = @"
                    INSERT INTO Products (Name, Type, Color, Calories)
                    VALUES
                        ('Apple', 'Fruit', 'Red', 52),
                        ('Banana', 'Fruit', 'Yellow', 89),
                        ('Carrot', 'Vegetable', 'Orange', 41),
                        ('Tomato', 'Vegetable', 'Red', 18)";

                SqlCommand command = new SqlCommand(insertDataQuery, connection);
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} rows inserted into 'Products'.");
            }
        }

        public static DataTable GetAllProducts()
        {
            DataTable table = new DataTable();
            using (SqlConnection connection = GetConnection())
            {
                string query = "SELECT * FROM Products";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);
            }
            return table;
        }

        public static void CloseConnection(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
    }
}