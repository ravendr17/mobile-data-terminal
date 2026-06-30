using Backend.Authentication;
using Backend.Data;
using Backend.DTOs;
using Backend.Entities;
using Backend.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class AccountService(
    AppDbContext context,
    PasswordHasher<Account> hasher,
    TokenProvider tokenProvider)
{
    private readonly AppDbContext _context = context;
    private readonly PasswordHasher<Account> _hasher = hasher;
    private readonly TokenProvider _tokenProvider = tokenProvider;

    public async Task<Result<int>> CreateAsync(AccountCreateRequest req)
    {
        var accounts = _context.Accounts;
        var licenses = _context.Licenses;
        
        bool usernameAlreadyExists = await accounts
            .AnyAsync(a => a.Username == req.Username);

        if (usernameAlreadyExists)
        {
            return Result<int>.Conflict($"Username {req.Username} already exists.");
        }

        bool emailAlreadyExists = await accounts
            .AnyAsync(a => a.Email == req.Email);

        if (emailAlreadyExists)
        {
            return Result<int>.Conflict($"Email {req.Email} already exists.");
        }

        bool accountRoleExists = await _context.AccountRoles
            .AnyAsync(ar => ar.Id == req.RoleId);

        if (!accountRoleExists)
        {
            return Result<int>.NotFound($"Account role ID {req.RoleId} not found.");
        }

        int? licenseId = null;

        if (!string.IsNullOrWhiteSpace(req.LicenseNumber))
        {
            licenseId = await licenses
                .Where(l => l.Number == req.LicenseNumber)
                .Select(l => (int?)l.Id)
                .FirstOrDefaultAsync();

            if (licenseId == null)
            {
                return Result<int>.NotFound($"License {req.LicenseNumber} not found.");
            }

            bool licenseAlreadyLinked = await accounts
                .AnyAsync(a => a.LicenseId == licenseId);

            if (licenseAlreadyLinked)
            {
                return Result<int>
                    .Conflict($"License {req.LicenseNumber} is already linked to another account");
            }
        }

        Account account = new Account
        {
            Email = req.Email,
            Username = req.Username,
            RoleId = req.RoleId,
            LicenseId = licenseId
        };

        account.Password = _hasher.HashPassword(account, req.Password);

        await accounts.AddAsync(account);
        await _context.SaveChangesAsync();

        return Result<int>.Success(account.Id);
    }

    public async Task<Result<AccountGetResponse>> GetByIdAsync(int id)
    {
        var account = await _context.Accounts
            .Where(a => a.Id == id)
            .Select(a => new AccountGetResponse(
                a.Id,
                a.Username,
                a.Email,
                a.RoleId,
                a.Role.Name,
                a.LicenseId,
                a.License!.Number
            ))
            .FirstOrDefaultAsync();

        if (account == null)
        {
            return Result<AccountGetResponse>.NotFound($"Account ID {id} not found");
        }

        return Result<AccountGetResponse>.Success(account);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        int rows = await _context.Accounts
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();

        if (rows > 0) return Result.Success();

        return Result.NotFound($"Account ID {id} not found.");
    }

    public async Task<Result<string>> LoginAsync(AccountLoginRequest req)
    {
        var account = await _context.Accounts
            .Include(a => a.Role)
            .FirstOrDefaultAsync(a => a.Email == req.Identifier || a.Username == req.Identifier);

        if (account == null)
        {
            return Result<string>.Unauthorized("Invalid username/email or password.");
        }

        var result = _hasher.VerifyHashedPassword(
            account, account.Password, req.Password
        );

        if (result == PasswordVerificationResult.Failed)
        {
            return Result<string>.Unauthorized("Invalid username/email or password.");
        }

        string token = _tokenProvider.Create(account);

        return Result<string>.Success(token);
    }
}