using APBD_Zaj8.Services.Post;
using Microsoft.Data.SqlClient;

using APBD_Zaj8;

namespace APBD_Zaj8.Services.Put;

public class AddToTripService : IAddToTrip
{
    public async Task<AddToTripStatus> AddToTrip(int tripId, int clientId)
    {
        
        string addToTripCommand = "Insert into Client_Trip (IdClient, IdTrip, RegisteredAt) values (@clientId, @tripId, @date);";
        
        DateTime date = DateTime.Now;
        
        string mergedDate = date.ToString("yyyyMMdd");
        Console.WriteLine(mergedDate);
        int dateInt = int.Parse(mergedDate);

        using (SqlConnection connection = TripsDBConnection.GetConnection()) 
        {
            await connection.OpenAsync();
            
            if (!await Utils.ClientExists(connection, clientId))
            {
                return AddToTripStatus.ClientNotFound;
            }
            else if (!await Utils.TripExists(connection, tripId))
            {
                return AddToTripStatus.TripNotFound;
            }
            else if (!await isMaximumNotReached(connection, tripId))
            {
                return AddToTripStatus.MaximumClientsReached;
            }

            using (SqlCommand command = new SqlCommand(addToTripCommand, connection))
            {
                command.Parameters.AddWithValue("@clientId", clientId);
                command.Parameters.AddWithValue("@tripId", tripId);
                command.Parameters.AddWithValue("@date", dateInt);
                
                await command.ExecuteNonQueryAsync();
            }
            return AddToTripStatus.Success;
        }
    }
    

    private async Task<bool> isMaximumNotReached(SqlConnection connection, int tripId)
    {
        string peopleRegisteredCommand = "Select Count(1) From Client_Trip Where Client_Trip.IdTrip = @tripId";
        string maximumPeopleCommand = "Select Trip.MaxPeople From Trip Where Trip.IdTrip = @tripId;";

        using (SqlCommand registeredCommand = new SqlCommand(peopleRegisteredCommand, connection))
        using (SqlCommand maximumCommand = new SqlCommand(maximumPeopleCommand, connection))
        {
            registeredCommand.Parameters.AddWithValue("@tripId", tripId);
            maximumCommand.Parameters.AddWithValue("@tripId", tripId);
            
            int? registeredResult = (int?) (await registeredCommand.ExecuteScalarAsync());
            int? maximumResult = (int?) (await maximumCommand.ExecuteScalarAsync());
            
            if (registeredResult >= maximumResult)
            {
                return false;
            }

            return true;
        }
    }
    
}


