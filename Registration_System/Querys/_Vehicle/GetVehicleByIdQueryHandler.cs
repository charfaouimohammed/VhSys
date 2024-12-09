using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Querys._Vehicle
{
    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, VehicleDTO>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetVehicleByIdQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<VehicleDTO> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _context.Vehicles
                .Where(v => v.VehicleId == request.VehicleId && v.IsDeleted == false)  // Only non-deleted vehicle
                .Include(v => v.CurrentOwner)  // Include CurrentOwner if available
                .Include(v => v.RegistrationOffice)  // Include RegistrationOffice if available
                .Include(v => v.LicensePlates)  // Include LicensePlates
                .Include(v => v.OwnershipHistories)
                    .ThenInclude(oh => oh.Owner)  // Include OwnershipHistories with Owners
                .FirstOrDefaultAsync(cancellationToken);

            if (vehicle == null)
            {
                return null;  // Return null if the vehicle is not found or is deleted
            }

            return new VehicleDTO
            {
                VehicleId = vehicle.VehicleId,
                Make = vehicle.Make,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Color = vehicle.Color,
                CurrentOwner = vehicle.CurrentOwner != null ? vehicle.CurrentOwner.Name : null,
                RegistrationOffice = vehicle.RegistrationOffice != null ? vehicle.RegistrationOffice.OfficeName : null,
                LicensePlateNumber = vehicle.LicensePlates.FirstOrDefault() != null ? vehicle.LicensePlates.FirstOrDefault().PlateNumber : null,
                OwnershipHistory = vehicle.OwnershipHistories
                    .Where(oh => oh.IsDeleted == false) // Only include non-deleted ownership histories
                    .Select(oh => new OwnershipHistoryDTO
                    {
                        OwnershipId = oh.OwnershipId,
                        VehicleId = oh.VehicleId,
                        OwnerId = oh.OwnerId,
                        StartDate = oh.StartDate,
                        EndDate = oh.EndDate,
                        OwnerName = oh.Owner != null ? oh.Owner.Name : null,
                        IsCurrentOwner = oh.IsCurrentOwner,
                        IsDeleted = oh.IsDeleted
                    })
                    .ToList() // Ensure this is a list
            };
        }
    }
}
