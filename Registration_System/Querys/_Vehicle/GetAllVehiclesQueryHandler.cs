using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Querys._Vehicle
{
    public class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, List<VehicleDTO>>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetAllVehiclesQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<VehicleDTO>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Vehicles
                .Where(v => v.IsDeleted == false)  // Exclude deleted vehicles
                .Include(v => v.CurrentOwner)  // Include CurrentOwner if available
                .Include(v => v.RegistrationOffice)  // Include RegistrationOffice if available
                .Include(v => v.LicensePlates)  // Include LicensePlates
                .Include(v => v.OwnershipHistories)
                    .ThenInclude(oh => oh.Owner)  // Include OwnershipHistories with Owners
                .Select(v => new VehicleDTO
                {
                    VehicleId = v.VehicleId,
                    Make = v.Make,
                    Model = v.Model,
                    Year = v.Year,
                    Color = v.Color,
                    // Handle null checks for CurrentOwner and RegistrationOffice
                    CurrentOwner = v.CurrentOwner != null ? v.CurrentOwner.Name : null,
                    RegistrationOffice = v.RegistrationOffice != null ? v.RegistrationOffice.OfficeName : null,
                    LicensePlateNumber = v.LicensePlates.FirstOrDefault() != null ? v.LicensePlates.FirstOrDefault().PlateNumber : null,
                    OwnershipHistory = v.OwnershipHistories
                        .Where(oh => oh.IsDeleted == false) // Only include non-deleted ownership histories
                        .Select(oh => new OwnershipHistoryDTO
                        {
                            OwnershipId = oh.OwnershipId,
                            VehicleId = oh.VehicleId,
                            OwnerId = oh.OwnerId,
                            StartDate = oh.StartDate,
                            EndDate = oh.EndDate,
                            OwnerName = oh.Owner != null ? oh.Owner.Name : null,  // Handle null Owner
                            IsCurrentOwner = oh.IsCurrentOwner,
                            IsDeleted = oh.IsDeleted
                        })
                        .ToList() // Ensure this is a list
                })
                .ToListAsync(cancellationToken);
        }
    }
}
