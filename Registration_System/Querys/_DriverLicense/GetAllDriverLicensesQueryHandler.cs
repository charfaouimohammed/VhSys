using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;
using Registration_System.Querys._DriverLicense;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Queries._DriverLicense
{
    public class GetAllDriverLicensesQueryHandler : IRequestHandler<GetAllDriverLicensesQuery, List<DriverLicenseDTO>>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetAllDriverLicensesQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<DriverLicenseDTO>> Handle(GetAllDriverLicensesQuery request, CancellationToken cancellationToken)
        {
            var licenses = await _context.DriverLicenses
                .Include(dl => dl.LicenseCategoriesNavigation)
                .ThenInclude(lc => lc.Categorie)
                .ToListAsync(cancellationToken);

            var licenseDtos = licenses.Select(license => new DriverLicenseDTO
            {
                LicenseId = license.LicenseId,
                LicenseNumber = license.LicenseNumber,
                IssueDate = license.IssueDate ?? new DateOnly(2000, 1, 1),
                ExpiryDate = license.ExpiryDate ?? new DateOnly(2000, 1, 1),
                Categories = license.LicenseCategoriesNavigation.Select(lc => lc.Categorie.Name).ToList()
            }).ToList();

            return licenseDtos;
        }
    }
}
