using Backend.DTOs;
using Backend.Services;
using Backend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountsController(AccountService accountService): ControllerBase
{
    private readonly AccountService _accountService = accountService;

    [HttpPost]
    public async Task<ActionResult> CreateAsync(AccountCreateRequest request)
    {
        var result = await _accountService.CreateAsync(request);
        
        if (!result.IsSuccess) return result.ErrorResponse();

        return CreatedAtAction(
            "GetById",
            new { id = result.Data},
            new { id = result.Data }
        );
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> GetByIdAsync(int id)
    {
        var result = await _accountService.GetByIdAsync(id);

        return result.IsSuccess ? Ok(result.Data) : result.ErrorResponse();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var result = await _accountService.DeleteAsync(id);

        return result.IsSuccess ? NoContent() : result.ErrorResponse();
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginAsync(AccountLoginRequest request)
    {
        var result = await _accountService.LoginAsync(request);

        if (!result.IsSuccess) return result.ErrorResponse();

        return Ok(result.Data);
    }
}