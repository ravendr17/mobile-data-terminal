using Backend.DTOs;
using Backend.Services;
using Backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountsControllers(AccountService accountService): ControllerBase
{
    private AccountService _accountService = accountService;

    [HttpPost]
    public async Task<ActionResult> CreateAsync(AccountCreateRequest request)
    {
        var result = await _accountService.CreateAsync(request);
        
        if (!result.IsSuccess) return result.ErrorResponse();

        return CreatedAtAction(
            nameof(GetByIdAsync),
            new { id = result.Data},
            new { id = result.Data }
        );
    }

    [HttpGet("{id:int}")]
    [ActionName(nameof(GetByIdAsync))]
    public async Task<ActionResult> GetByIdAsync(int id)
    {
        var result = await _accountService.GetByIdAsync(id);

        return result.IsSuccess ? Ok(result.Data) : result.ErrorResponse();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var result = await _accountService.DeleteAsync(id);

        return result.IsSuccess ? NoContent() : result.ErrorResponse();
    }
}