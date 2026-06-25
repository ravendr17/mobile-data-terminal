using Backend.DTOs;
using Backend.Services;
using Backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehiclesController(VehicleService vehicleService): ControllerBase
{
    private readonly VehicleService _vehicleService = vehicleService;

    [HttpGet("{number}")]
    public async Task<ActionResult> GetByPlateOrMvFileNumAsync(string number)
    {
        var result = await _vehicleService.GetByPlateOrMvFileNumAsync(number);

        return result.IsSuccess ? Ok(result.Data): result.ErrorResponse();
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(VehicleCreateRequest request)
    {
        var result = await _vehicleService.CreateAsync(request);

        if (!result.IsSuccess) return result.ErrorResponse();

        return CreatedAtAction(
            "GetByPlateOrMvFileNum",
            new { number = request.PlateNumber},
            new { id = result.Data}
        );
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var result = await _vehicleService.DeleteAsync(id);
        
        return result.IsSuccess ? NoContent(): result.ErrorResponse();
    }
}