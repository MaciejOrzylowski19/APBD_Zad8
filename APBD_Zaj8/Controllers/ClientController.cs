using APBD_Zaj8.Models;
using ApBD_Zaj8.Services;
using APBD_Zaj8.Services.Delete;
using APBD_Zaj8.Services.Post;
using APBD_Zaj8.Services.Put;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Zaj8.Controllers;

[ApiController]
[Route("api/clients")]
class ClientController : ControllerBase
{
    
    private IDeleteFromTrip _deleteFromTripService;
    private IAddClient _addClientService;
    private IAddToTrip _addToTripService;
    private IClientTripService _clientTripService;
    
    public ClientController(IClientTripService clientTripService, IAddClient addClientService, IAddToTrip addToTripService, IDeleteFromTrip deleteFromTripService)
    {
        _clientTripService = clientTripService;
        _addClientService = addClientService;
        _addToTripService = addToTripService;
        _deleteFromTripService = deleteFromTripService;
    }

    [HttpGet]
    [Route("{id}/trips")]
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
    
    
    [HttpPut]
    [Route("{id}/trips/{tripId}")]
    public async Task<IActionResult> RegisterToTrip([FromBody] ClientDTO client, int id, int tripId)
    {
        var result = await _addToTripService.AddToTrip(tripId, id);

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
    
    [HttpDelete]
    [Route("{id}/trips/{tripId}")]
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