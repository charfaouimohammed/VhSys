using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;

namespace Registration_System.Querys._Owner
{
    public class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, OwnerDto>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetOwnerByIdQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<OwnerDto> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
        {
            var owner = await _context.Owners
                .Where(o => o.OwnerId == request.OwnerId && o.IsDeleted == false)
                .Include(o => o.Notifications)
                .Include(o => o.OwnershipHistories)
                .Include(o => o.Vehicles)
                .Include(o => o.DriverLicenses) // Include DriverLicenses
                    .ThenInclude(dl => dl.LicenseCategoriesNavigation) // Include LicenseCategoriesNavigation
                    .ThenInclude(lc => lc.Categorie) // Include Categorie for category names
                .FirstOrDefaultAsync(cancellationToken);

            if (owner == null)
            {
                return null;
            }

            return new OwnerDto
            {
                OwnerId = owner.OwnerId,
                Cin = owner.Cin,
                Name = owner.Name,
                DateOfBirth = owner.DateOfBirth,
                Address = owner.Address,
                PhoneNumber = owner.PhoneNumber,
                Email = owner.Email,
                Notifications = owner.Notifications.Select(n => new NotificationDTO
                {
                    NotificationId = n.NotificationId,
                    Message = n.Message,
                    Timestamp = n.Timestamp,
                    Status = n.Status,
                    IsDeleted = n.IsDeleted
                }).ToList(),
                OwnershipHistories = owner.OwnershipHistories.Select(oh => new OwnershipHistoryDTO
                {
                    OwnershipId = oh.OwnershipId,
                    VehicleId = oh.VehicleId,
                    StartDate = oh.StartDate,
                    EndDate = oh.EndDate,
                    IsDeleted = oh.IsDeleted,
                    IsCurrentOwner = oh.IsCurrentOwner
                }).ToList(),
                Vehicles = owner.Vehicles.Select(v => new VehicleDTO
                {
                    VehicleId = v.VehicleId,
                    Make = v.Make,
                    Model = v.Model,
                    Year = v.Year,
                    Color = v.Color,
                }).ToList(),
                DriverLicenses = owner.DriverLicenses.Select(dl => new DriverLicenseDTO
                {
                    LicenseId = dl.LicenseId,
                    LicenseNumber = dl.LicenseNumber,
                    IssueDate = dl.IssueDate ?? new DateOnly(2000, 1, 1),
                    ExpiryDate = dl.ExpiryDate ?? new DateOnly(2000, 1, 1),
                    Categories = dl.LicenseCategoriesNavigation.Select(lc => lc.Categorie.Name).ToList()
                }).ToList()
            };
        }
    }
}
