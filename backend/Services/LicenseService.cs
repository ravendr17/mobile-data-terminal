using Backend.Data;
using Backend.DTOs;
using Backend.Entities;
using Backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class LicenseService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<Result<int>> CreateAsync(LicenseCreateRequest req)
    {
        var licenses = _context.Licenses;

        bool numberAlreadyExists = await licenses
            .AnyAsync(l => l.Number == req.Number);

        if (numberAlreadyExists)
        {
            return Result<int>.Conflict($"License {req.Number} already exists.");
        }

        int licenseTypeId = req.TypeId!.Value;
        int licenseStatusId = req.StatusId!.Value;

        bool typeExists = await _context.LicenseTypes
            .AnyAsync(lt => lt.Id == licenseTypeId);
        
        if (!typeExists)
        {
            return Result<int>.BadRequest($"License type {licenseTypeId} not found.");
        }

        bool statusExists = await _context.LicenseStatuses 
            .AnyAsync(ls => ls.Id == licenseStatusId);

        if (!statusExists)
        {
            return Result<int>.BadRequest($"License status {licenseStatusId} not found");
        }

        License license = new License
        {
            Number = req.Number,
            TypeId = licenseTypeId,
            StatusId = licenseStatusId,
            IssuanceDate = req.IssuanceDate,
            ExpirationDate = req.IssuanceDate.AddYears(req.Validity!.Value),
            FirstName = req.FirstName,
            MiddleName = req.MiddleName,
            LastName = req.LastName,
            BirthDate = req.BirthDate,
            Sex = req.Sex,
            Address = req.Address,
            Nationality = req.Nationality,
            EyeColor = req.EyeColor,
            Height = req.Height!.Value,
            Weight = req.Weight!.Value,
            BloodType = req.BloodType
        };

        await licenses.AddAsync(license);
        await _context.SaveChangesAsync();

        return Result<int>.Success(license.Id);
    }

    public async Task<Result<LicenseGetResponse>> GetByNumberAsync(string number)
    {
        var license = await _context.Licenses
            .Where(l => l.Number == number)
            .Select(l => new LicenseGetResponse(
                l.Id,
                l.Number,
                l.TypeId,
                l.Type.Name,
                l.StatusId,
                l.Status.Name,
                l.IssuanceDate,
                l.ExpirationDate,
                StringUtils.FullName(l.FirstName,
                                     l.MiddleName,
                                     l.LastName),
                l.BirthDate,
                l.Sex,
                l.Address,
                l.Nationality,
                l.EyeColor,
                l.Height,
                l.Weight,
                l.BloodType
            ))
            .FirstOrDefaultAsync();

        if (license == null)
        {
            return Result<LicenseGetResponse>.NotFound($"License {number} not found.");
        }

        return Result<LicenseGetResponse>.Success(license);
    }
    
    public async Task<Result> DeleteAsync(int id)
    {
        int rows = await _context.Licenses
            .Where(l => l.Id == id)
            .ExecuteDeleteAsync();

        if (rows > 0) return Result.Success();

        return Result.NotFound($"License ID {id} not found.");
    }
}