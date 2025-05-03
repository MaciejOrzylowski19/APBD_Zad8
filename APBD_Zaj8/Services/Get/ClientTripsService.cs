

using System.Data;
using ApBD_Zaj8.Services;

namespace APBD_Zaj8.Services;
using Microsoft.Data.SqlClient;
using APBD_Zaj8.Models;

public class ClientTripsService : IClientTripService
{
    
    
    private string _doClientExistsCommand = "Select COUNT(1) as DoExist from Client Where Client.IdClient = @client;";
    private string _doClientHaveTripsCommand = "Select COUNT(1) as DoExist from Trip Where Trip.IdClient = @client;";

    private string _getTripsCommand = """
                                      Select Trip.IdTrip, Trip.Name, Trip.Description, Trip.DateFrom, Trip.DateTo, Trip.MaxPeople, 
                                      Client_Trip.RegisteredAt, Client_Trip.PaymentDate
                                      From Trip inner join Client_Trip on Client_Trip.IdTrip = Trip.IdTrip 
                                      inner join Client on Client.IdClient = Client_Trip.IdClient
                                      Where Client.IdClient = @client;
                                      """;
        
    
    public async Task<List<TripSpecificationDTO>> GetTrips(int id)
    {
        
        List <TripSpecificationDTO> trips = new List<TripSpecificationDTO>();
        
        using (SqlConnection connection = TripsDBConnection.GetConnection())
        {
            await connection.OpenAsync();
            
            if (!await ClientExists(connection, id))
            {
                throw new ArgumentException("Client does not exist");
            }
            if (!await ClientHasTrips(connection, id))
            {
                throw new ArgumentException("Client does not have any trips");
            }
            
            using (SqlCommand command = new SqlCommand(_doClientExistsCommand, connection))
            {
               command.Parameters.AddWithValue("@client", id);
               
               SqlDataReader reader = await command.ExecuteReaderAsync();

               while (await reader.ReadAsync())
               {
                   TripDTO tripDto = new TripDTO()
                   {
                          Id = reader.GetInt32(reader.GetOrdinal("IdTrip")),
                          Name = reader.GetString(reader.GetOrdinal("Name")),
                          Description = reader.GetString(reader.GetOrdinal("Description")),
                          DateFrom = reader.GetDateTime(reader.GetOrdinal("DateFrom")),
                          DateTo = reader.GetDateTime(reader.GetOrdinal("DateTo")),
                          MaxPeople = reader.GetInt32(reader.GetOrdinal("MaxPeople"))
                   };
                   
                   TripSpecificationDTO tripSpecificationDto = new TripSpecificationDTO()
                   {
                       Trip = tripDto,
                       RegisteredAt = reader.GetInt32(reader.GetOrdinal("RegisteredAt")),
                       PaymentDate = reader.GetInt32(reader.GetOrdinal("PaymentDate"))
                   };
                     trips.Add(tripSpecificationDto);
               }
            }
        }
        return trips;
    }

    private async Task<bool> ClientHasTrips(SqlConnection connection, int id)
    {
        using (SqlCommand command = new SqlCommand(_doClientHaveTripsCommand, connection))
        {
            command.Parameters.AddWithValue("@client", id);
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (!reader.HasRows)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private async Task<bool> ClientExists(SqlConnection connection, int id)
    {
        using (SqlCommand command = new SqlCommand(_doClientExistsCommand, connection))
        {
            command.Parameters.AddWithValue("@client", id);
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (!reader.HasRows)
                {
                    return false;
                }
            }
        }
        return true;
    }
    
    
}