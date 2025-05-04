using APBD_Zaj8.Services;
using Microsoft.AspNetCore.Mvc;
using APBD_Zaj8.Services;

namespace APBD_Zaj8.Controllers;



[Route("api/trips")]
[ApiController]
public class TripController : ControllerBase
{
    
    
    private readonly ITripsList _clientTripService;
    
    public TripController(ITripsList clientTripService)
    {
        _clientTripService = clientTripService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips()
    {
        Console.WriteLine("We are in the TripController");
        
        var result = await _clientTripService.GetTrips();
        return Ok(result);
    }
    
}