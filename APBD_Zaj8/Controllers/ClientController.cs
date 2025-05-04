using APBD_Zaj8.Models;
using ApBD_Zaj8.Services;
using APBD_Zaj8.Services.Delete;
using APBD_Zaj8.Services.Post;
using APBD_Zaj8.Services.Put;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Zaj8.Controllers;

[Route("api/[controller]/{id}/trips")]
[ApiController]
public class ClientController : ControllerBase
{
    
    private IClientTripService _clientTripService;
    
    public ClientController(IClientTripService clientTripService)
    {
        _clientTripService = clientTripService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTrips(int id)
    {
        List<TripSpecificationDTO> trips;
        
        try
        {
            trips = await _clientTripService.GetTrips(id);
        }
        catch (ArgumentNullException ex)
        {
            return NotFound("Client has no trips");
        }
        catch (ArgumentException ex)
        {
            return NotFound("Client does not exist");
        }
        return Ok(trips);
    }
    
}

[Route("api/clients")]
[ApiController]
public class ClientAddController : ControllerBase
{
    
    private IAddClient _addClientService;
    
    public ClientAddController(IAddClient addClientService)
    {
        _addClientService = addClientService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddClient([FromBody] ClientDTO client)
    {
        
        try
        {
            await _addClientService.AddClient(client);
        }
        catch (ArgumentException ex)
        {
            return BadRequest("Client already exists");
        }
        return Ok();
    }
}



[Route("api/clients/{id}/trips/{tripId}")]
[ApiController]
public class RegisterToTripController : ControllerBase
{
    private IAddToTrip _registerToTripService;
    
    public RegisterToTripController(IAddToTrip registerToTripService)
    {
        _registerToTripService = registerToTripService;
    }

    [HttpPut]
    public async Task<IActionResult> RegisterToTrip([FromBody] ClientDTO client, int id, int tripId)
    {
        var result = await _registerToTripService.AddToTrip(tripId, id);

        switch (result)
        {
            case AddToTripStatus.TripNotFound:
                return NotFound("Trip does not exist");
            case AddToTripStatus.ClientNotFound:
                return NotFound("Client does not exist");
            case AddToTripStatus.MaximumClientsReached:
                return BadRequest("Maximum number of clients reached");
        }

        return Ok();
    }
}

[Route("api/clients/{id}/trips/{tripId}")]
[ApiController]
public class DeleteFromTripController : ControllerBase
{
    private IDeleteFromTrip _deleteFromTripService;

    public DeleteFromTripController(IDeleteFromTrip deleteFromTripService)
    {
        _deleteFromTripService = deleteFromTripService;
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteFromTrip(int tripId, int id)
    {
        var result = await _deleteFromTripService.DeleteFromTrip(tripId, id);
        
        switch (result)
        {
            case DeleteFromTripStatus.TripNotFound:
                return NotFound("Trip does not exist");
            case DeleteFromTripStatus.ClientNotFound:
                return NotFound("Client does not exist");
        }
        return Ok();
    }
}