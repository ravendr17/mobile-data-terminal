using Backend.Data;
using Backend.DTOs;
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
}