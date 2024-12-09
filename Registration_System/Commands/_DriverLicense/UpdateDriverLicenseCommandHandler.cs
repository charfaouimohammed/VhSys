using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Commands._DriverLicense
{
    public class UpdateDriverLicenseCommandHandler : IRequestHandler<UpdateDriverLicenseCommand, Unit>
    {
        private readonly RegistrationSystemDbContext _context;

        public UpdateDriverLicenseCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateDriverLicenseCommand request, CancellationToken cancellationToken)
        {
            // Find the existing license
            var license = await _context.DriverLicenses
                .FirstOrDefaultAsync(dl => dl.LicenseId == request.LicenseId, cancellationToken);

            if (license == null)
            {
                throw new Exception("Driver License not found");
            }

            // Update license details
            license.LicenseNumber = request.LicenseNumber;
            license.IssueDate = request.IssueDate;
            license.ExpiryDate = request.ExpiryDate;

            // Update the license in the database
            _context.DriverLicenses.Update(license);
            await _context.SaveChangesAsync(cancellationToken);

            // Remove existing categories
            var existingCategories = await _context.LicenseCategories
                .Where(lc => lc.LicenseId == request.LicenseId)
                .ToListAsync(cancellationToken);

            _context.LicenseCategories.RemoveRange(existingCategories);

            // Add new categories
            foreach (var categoryId in request.CategoryIds)
            {
                var licenseCategory = new LicenseCategory
                {
                    LicenseId = request.LicenseId,
                    CategorieId = categoryId
                };
                _context.LicenseCategories.Add(licenseCategory);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value; // Indicating the update operation is completed
        }
    }
}
