using Microsoft.Data.SqlClient;

namespace APBD_Zaj8.Services.Delete;

public class DeleteFromTripService : IDeleteFromTrip
{

    private static string commandString = "Delete From Client_Trip Where Client_Trip.IdClient = @clientId and Client_Trip.IdTrip = @tripId; ";
    
    public async Task<DeleteFromTripStatus> DeleteFromTrip(int tripId, int clientId)
    {
        
        using (SqlConnection connection = TripsDBConnection.GetConnection()) 
        {
            await connection.OpenAsync();
            
            if (!Utils.ClientExists(connection, clientId).Result)
            {
                return DeleteFromTripStatus.ClientNotFound;
            }
            else if (!Utils.TripExists(connection, tripId).Result)
            {
                return DeleteFromTripStatus.TripNotFound;
            }
            
            using (SqlCommand command = new SqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@clientId", clientId);
                command.Parameters.AddWithValue("@tripId", tripId);
                
                command.ExecuteNonQuery();
            }

            return DeleteFromTripStatus.Success;
        }
    }
}