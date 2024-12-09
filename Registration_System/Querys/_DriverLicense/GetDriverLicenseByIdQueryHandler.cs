using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Queries._DriverLicense
{
    public class GetDriverLicenseByIdQueryHandler : IRequestHandler<GetDriverLicenseByIdQuery, DriverLicenseDTO>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetDriverLicenseByIdQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<DriverLicenseDTO> Handle(GetDriverLicenseByIdQuery request, CancellationToken cancellationToken)
        {
            var license = await _context.DriverLicenses
                .Include(dl => dl.LicenseCategoriesNavigation)
                .ThenInclude(lc => lc.Categorie)
                .FirstOrDefaultAsync(dl => dl.LicenseId == request.LicenseId, cancellationToken);

            if (license == null)
            {
                throw new Exception("Driver License not found");
            }

            var categories = license.LicenseCategoriesNavigation.Select(lc => lc.Categorie.Name).ToList();

            var licenseDto = new DriverLicenseDTO
            {
                LicenseId = license.LicenseId,
                LicenseNumber = license.LicenseNumber,
                IssueDate = license.IssueDate ?? new DateOnly(2000, 1, 1),
                ExpiryDate = license.ExpiryDate ?? new DateOnly(2000, 1, 1),
                Categories = categories
            };

            return licenseDto;
        }
    }
}
