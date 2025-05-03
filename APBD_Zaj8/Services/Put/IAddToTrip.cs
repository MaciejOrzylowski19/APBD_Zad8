namespace APBD_Zaj8.Services.Put;

public interface IAddToTrip
{
    
    Task<AddToTripStatus> AddToTrip(int tripId, int clientId);
    
}

public enum AddToTripStatus
{
    Success,
    TripNotFound,
    ClientNotFound,
    MaximumClientsReached,
}