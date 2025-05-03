namespace APBD_Zaj8.Services;
using Microsoft.Data.SqlClient;


public class Utils
{
    
     public static async Task<bool> ClientExists(SqlConnection connection, int clientId)
    {
        string commandText = "Select 1 From Client Where exists (Select 1 From Client where Client.IdClient = @clientId);";
        
        using (SqlCommand command = new SqlCommand(commandText, connection)) {
            
            command.Parameters.AddWithValue("@clientId", clientId);
            
            int? result =  (int?) (await command.ExecuteScalarAsync());
            if (result == null || result == 0)
            {
                return false;
            }
        }
        return true;
    }
    
    public static async Task<bool> TripExists(SqlConnection connection, int tripId)
    {
        string commandText = "Select 1 From Trip Where exists (Select 1 From Trip where Trip.IdTrip = @tripId);";
        
        using (SqlCommand command = new SqlCommand(commandText, connection)) {
            
            command.Parameters.AddWithValue("@tripId", tripId);
            
            int? result =  (int?) (await command.ExecuteScalarAsync());
            if (result == null || result == 0)
            {
                return false;
            }
        }
        return true;
    }
    
}