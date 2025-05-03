using APBD_Zaj8.Models;
using Microsoft.Data.SqlClient;

namespace APBD_Zaj8.Services;

public class TripsList : ITripsList
{

    private string _queryCommand =
        "Select Trip.IdTrip, Trip.Name, Trip.Description, Trip.DateFrom, Trip.DateTo, Trip.MaxPeople from Trip";

    public async Task<List<TripDTO>> GetTrips()
    {
        List<TripDTO> trips = new List<TripDTO>();
        using (SqlConnection connection = TripsDBConnection.GetConnection())
        using (SqlCommand command = new SqlCommand(_queryCommand, connection))
        {

            await connection.OpenAsync();

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    TripDTO trip = new TripDTO()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("IdTrip")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        DateFrom = reader.GetDateTime(reader.GetOrdinal("DateFrom")),
                        DateTo = reader.GetDateTime(reader.GetOrdinal("DateTo")),
                        MaxPeople = reader.GetInt32(reader.GetOrdinal("MaxPeople"))
                    };

                    trips.Add(trip);
                }

            }
        }

        return trips;
    }
    

}