using Backend.Data;
using Backend.DTOs;
using Backend.Entities;
using Backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class VehicleService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<Result<VehicleGetResponse>> GetByPlateOrMvFileNumAsync(string number)
    {
        var vehicle = await _context.Vehicles
            .Where(v => v.PlateNumber == number || v.MvFileNumber == number)
            .Select(v => new VehicleGetResponse(
                v.Id,
                v.PlateNumber,
                v.MvFileNumber,
                v.Vin,
                v.RegisterIssuanceDate,
                v.RegisterExpiryDate,
                v.Make,
                v.Model,
                v.Year,
                v.Color,
                StringUtils.FullName(v.License.FirstName,
                                     v.License.MiddleName,
                                     v.License.LastName),
                v.License.Number,
                v.License.Address
            ))
            .FirstOrDefaultAsync();

        if (vehicle == null)
        {
            return Result<VehicleGetResponse>.NotFound($"Vehicle with Plate / MV File {number} not found.");
        }

        return Result<VehicleGetResponse>.Success(vehicle);
    }

    public async Task<Result<int>> CreateAsync(VehicleCreateRequest req)
    {
        var vehicles = _context.Vehicles;
        var licenses = _context.Licenses;

        var licenseId = await licenses
            .Where(l => l.Number == req.LicenseNumber)
            .Select(l => (int?) l.Id)
            .FirstOrDefaultAsync();

        if (licenseId == null)
        {
            return Result<int>.NotFound($"License {req.LicenseNumber} not found.");
        }

        bool plateAlreadyExists = await vehicles
            .Where(v => v.PlateNumber == req.PlateNumber)
            .AnyAsync();
        
        if (plateAlreadyExists)
        {
            return Result<int>.Conflict($"Vehicle with plate {req.PlateNumber} already exists.");
        }

        bool mvFileAlreadyExists = await vehicles
            .Where(v => v.MvFileNumber == req.MvFileNumber)
            .AnyAsync();

        if (mvFileAlreadyExists)
        {
            return Result<int>.Conflict($"Vehicle with MV File {req.MvFileNumber} already exists.");
        }

        bool vinAlreadyExists = await vehicles
            .Where(v => v.Vin == req.Vin)
            .AnyAsync();

        if (vinAlreadyExists)
        {
            return Result<int>.Conflict($"Vehicle with VIN {req.Vin} already exists.");
        }

        Vehicle vehicle = new Vehicle
        {
            PlateNumber = req.PlateNumber,
            MvFileNumber = req.MvFileNumber,
            Vin = req.Vin,
            RegisterIssuanceDate = req.RegisterIssuanceDate,
            RegisterExpiryDate = req.RegisterIssuanceDate.AddYears(req.Validity!.Value),
            Make = req.Make,
            Model = req.Model,
            Year = req.Year!.Value,
            Color = req.Color,
            LicenseId = licenseId.Value
        };

        await vehicles.AddAsync(vehicle);
        await _context.SaveChangesAsync();

        return Result<int>.Success(vehicle.Id);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        int rowsAffected = await _context.Vehicles
            .Where(v => v.Id == id)
            .ExecuteDeleteAsync();

        if (rowsAffected > 0) return Result.Success();

        return Result.NotFound($"Vehicle ID {id} not found.");
    }
}