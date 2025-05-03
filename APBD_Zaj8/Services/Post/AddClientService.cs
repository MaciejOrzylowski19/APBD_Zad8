using Microsoft.Data.SqlClient;

namespace APBD_Zaj8.Services.Post;

public class AddClientService : IAddClient
{

    private static string _command =
        "Insert Into Client (FirstName, LastName, Email, Pesel, Telephone) Values (@firstName, @lastName, @email, @pesel, @telephone); SELECT SCOPE_IDENTITY();";
    
    
    
    public async Task<int> AddClient(string firstName, string lastName, string email, string telephone, string pesel)
    {
        
        using (SqlConnection connection = TripsDBConnection.GetConnection())
        using (SqlCommand command = new SqlCommand(_command, connection))
        {
            await connection.OpenAsync();
            
            command.Parameters.AddWithValue("@firstName", firstName);
            command.Parameters.AddWithValue("@lastName", lastName);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@pesel", pesel);
            command.Parameters.AddWithValue("@telephone", telephone);

            object? result = await command.ExecuteNonQueryAsync();
            if (result == null)
            {
                throw new ArgumentException("Client could not be added");
            }
            return Convert.ToInt32(result);
            
        }
    }
    
}