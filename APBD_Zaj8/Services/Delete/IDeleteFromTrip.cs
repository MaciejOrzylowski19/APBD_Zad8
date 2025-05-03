namespace APBD_Zaj8.Services.Delete;

public interface IDeleteFromTrip
{
    
    Task<DeleteFromTripStatus> DeleteFromTrip(int tripId, int clientId);
    
    
}

public enum DeleteFromTripStatus
{
    Success,
    TripNotFound,
    ClientNotFound,
}