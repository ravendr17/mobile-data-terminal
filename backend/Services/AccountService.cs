using Backend.Data;
using Backend.DTOs;
using Backend.Entities;
using Backend.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class AccountService(AppDbContext context)
{
    private AppDbContext _context = context;
    private PasswordHasher<Account> _hasher = new PasswordHasher<Account>();

    public async Task<Result<int>> CreateAsync(AccountCreateRequest req)
    {
        var accounts = _context.Accounts;
        var licenses = _context.Licenses;

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
            RoleId = req.RoleId,
            LicenseId = licenseId
        };

        account.Password = _hasher.HashPassword(account, req.Password);

        await accounts.AddAsync(account);
        await _context.SaveChangesAsync();

        return Result<int>.Success(account.Id);
    }
}