using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Querys._Owner
{
    public class GetAllOwnersQueryHandler : IRequestHandler<GetAllOwnersQuery, List<OwnerDto>>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetAllOwnersQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<OwnerDto>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
        {
            var owners = await _context.Owners
                .Where(o => o.IsDeleted == false) // Ensure deleted owners are excluded
                .Include(o => o.Notifications)
                .Include(o => o.OwnershipHistories)
                .Include(o => o.Vehicles)
                .Include(o => o.DriverLicenses) // Include DriverLicenses
                    .ThenInclude(dl => dl.LicenseCategoriesNavigation) // Include LicenseCategoriesNavigation for the category names
                    .ThenInclude(lc => lc.Categorie) // Include Categorie to get the category names
                .ToListAsync(cancellationToken);

            return owners.Select(o => new OwnerDto
            {
                OwnerId = o.OwnerId,
                Cin = o.Cin,
                Name = o.Name,
                DateOfBirth = o.DateOfBirth,
                Address = o.Address,
                PhoneNumber = o.PhoneNumber,
                Email = o.Email,
                Notifications = o.Notifications.Where(n => n.IsDeleted == false).Select(n => new NotificationDTO
                {
                    NotificationId = n.NotificationId,
                    Message = n.Message,
                    Timestamp = n.Timestamp,
                    Status = n.Status,
                    IsDeleted = n.IsDeleted
                }).ToList(),
                OwnershipHistories = o.OwnershipHistories.Where(oh => oh.IsDeleted == false).Select(oh => new OwnershipHistoryDTO
                {
                    OwnershipId = oh.OwnershipId,
                    VehicleId = oh.VehicleId,
                    StartDate = oh.StartDate,
                    EndDate = oh.EndDate,
                    IsDeleted = oh.IsDeleted,
                    IsCurrentOwner = oh.IsCurrentOwner
                }).ToList(),
                Vehicles = o.Vehicles.Where(v => v.IsDeleted == false).Select(v => new VehicleDTO
                {
                    VehicleId = v.VehicleId,
                    Make = v.Make,
                    Model = v.Model,
                    Year = v.Year,
                    Color = v.Color,
                }).ToList(),
                DriverLicenses = o.DriverLicenses.Where(dl => dl.IsDeleted == false).Select(dl => new DriverLicenseDTO
                {
                    LicenseId = dl.LicenseId,
                    LicenseNumber = dl.LicenseNumber,
                    IssueDate = dl.IssueDate ?? new DateOnly(2000, 1, 1),
                    ExpiryDate = dl.ExpiryDate ?? new DateOnly(2000, 1, 1),
                    Categories = dl.LicenseCategoriesNavigation.Select(lc => lc.Categorie.Name).ToList() // No IsDeleted check for Category
                }).ToList()
            }).ToList();
        }

    }
}
