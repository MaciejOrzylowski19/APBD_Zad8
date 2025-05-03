namespace APBD_Zaj8.Services;

using APBD_Zaj8.Models;

public interface ITripsList
{
    public Task<List<TripDTO>> GetTrips();
    
}