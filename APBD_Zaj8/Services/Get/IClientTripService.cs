

namespace ApBD_Zaj8.Services;
using APBD_Zaj8.Models;

public interface IClientTripService
{
     
     Task<List<TripSpecificationDTO>> GetTrips(int id);

}