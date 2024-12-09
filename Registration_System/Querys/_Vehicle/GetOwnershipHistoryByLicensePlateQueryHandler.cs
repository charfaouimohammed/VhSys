using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;

namespace Registration_System.Querys._Vehicle
{
    public class GetOwnershipHistoryByLicensePlateQueryHandler : IRequestHandler<GetOwnershipHistoryByLicensePlateQuery, List<OwnershipHistoryDTO>>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetOwnershipHistoryByLicensePlateQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<OwnershipHistoryDTO>> Handle(GetOwnershipHistoryByLicensePlateQuery request, CancellationToken cancellationToken)
        {
            // Fetch the vehicle by license plate
            var vehicle = await _context.Vehicles
                .Include(v => v.LicensePlates)
                .Include(v => v.OwnershipHistories)
                .ThenInclude(oh => oh.Owner)
                .FirstOrDefaultAsync(v => v.LicensePlates.Any(lp => lp.PlateNumber == request.LicensePlate), cancellationToken);

            if (vehicle == null)
            {
                return new List<OwnershipHistoryDTO>(); // Vehicle not found
            }

            // Map ownership histories to DTO
            var ownershipHistoryDtos = vehicle.OwnershipHistories
                .Where(oh => oh.IsDeleted == false)
                .Select(oh => new OwnershipHistoryDTO
                {
                    OwnershipId = oh.OwnershipId,
                    VehicleId = oh.VehicleId,
                    OwnerId = oh.OwnerId,
                    StartDate = oh.StartDate,
                    EndDate = oh.EndDate,
                    OwnerName = oh.Owner?.Name,
                    IsDeleted = oh.IsDeleted,
                    IsCurrentOwner = oh.IsCurrentOwner
                })
                .ToList();

            return ownershipHistoryDtos;
        }
    }
}
