using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Querys._Vehicle
{
    public class GetVehicleByLicensePlateNumberQueryHandler : IRequestHandler<GetVehicleByLicensePlateNumberQuery, VehicleDTO>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetVehicleByLicensePlateNumberQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<VehicleDTO> Handle(GetVehicleByLicensePlateNumberQuery request, CancellationToken cancellationToken)
        {
            // Include necessary relationships before projection
            var vehicle = await _context.Vehicles
                .Include(v => v.CurrentOwner)
                .Include(v => v.RegistrationOffice)
                .Include(v => v.LicensePlates)
                .Include(v => v.OwnershipHistories)
                .FirstOrDefaultAsync(v => v.LicensePlates.Any(lp => lp.PlateNumber == request.LicensePlateNumber) && v.IsDeleted == false, cancellationToken);

            if (vehicle == null)
                return null;

            // Map the entity to DTO
            return new VehicleDTO
            {
                VehicleId = vehicle.VehicleId,
                Make = vehicle.Make,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Color = vehicle.Color,
                IsDeleted = vehicle.IsDeleted,
                CurrentOwner = vehicle.CurrentOwner?.Name,
                RegistrationOffice = vehicle.RegistrationOffice?.OfficeName,
                LicensePlateNumber = vehicle.LicensePlates.FirstOrDefault()?.PlateNumber,
                OwnershipHistory = vehicle.OwnershipHistories
                    .Where(oh => oh.IsDeleted == false)
                    .Select(oh => new OwnershipHistoryDTO
                    {
                        OwnershipId = oh.OwnershipId,
                        StartDate = oh.StartDate,
                        EndDate = oh.EndDate,
                        IsCurrentOwner = oh.IsCurrentOwner
                    }).ToList()
            };
        }
    }
}
