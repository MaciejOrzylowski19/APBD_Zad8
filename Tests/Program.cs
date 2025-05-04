// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;

string connectionString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True";

using (var connection = new SqlConnection(connectionString))
{
    connection.Open();
    Console.WriteLine("Connected to the database.");
    
    string commandText = "SELECT IdTrip, Name FROM Trip";
    using (var command = new SqlCommand(commandText, connection))
    {
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                Console.WriteLine($"Id: {id}, Name: {name}");
            }
        }
    }
}
{
    
}