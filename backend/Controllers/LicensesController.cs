using Backend.DTOs;
using Backend.Services;
using Backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/licenses")]
public class LicensesController(LicenseService licenseService): ControllerBase
{
    private LicenseService _licenseService = licenseService;

    [HttpPost]
    public async Task<ActionResult> CreateAsync(LicenseCreateRequest request)
    {
        var result = await _licenseService.CreateAsync(request);

        if (!result.IsSuccess) return result.ErrorResponse();

        return CreatedAtAction(
            nameof(GetByNumberAsync),
            new { number = request.Number},
            new { id = result.Data }
        );
    }

    [HttpGet("{number}")]
    [ActionName(nameof(GetByNumberAsync))]
    public async Task<ActionResult> GetByNumberAsync(string number)
    {
        var result = await _licenseService.GetByNumberAsync(number);

        return result.IsSuccess ? Ok(result.Data) : result.ErrorResponse();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var result = await _licenseService.DeleteAsync(id);

        return result.IsSuccess ? NoContent() : result.ErrorResponse();
    }
}