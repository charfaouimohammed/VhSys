using MediatR;
using Registration_System.Models;

namespace Registration_System.Commands._DriverLicense
{
    public class CreateDriverLicenseCommandHandler : IRequestHandler<CreateDriverLicenseCommand, Guid>
    {
        private readonly RegistrationSystemDbContext _context;

        public CreateDriverLicenseCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateDriverLicenseCommand request, CancellationToken cancellationToken)
        {
            var license = new DriverLicense
            {
                LicenseId = Guid.NewGuid(),
                OwnerId = request.OwnerId,
                LicenseNumber = request.LicenseNumber,
                IssueDate = request.IssueDate,
                ExpiryDate = request.ExpiryDate,
                IsDeleted = false
            };

            _context.DriverLicenses.Add(license);
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var categoryId in request.CategoryIds)
            {
                var licenseCategory = new LicenseCategory
                {
                    LicenseId = license.LicenseId,
                    CategorieId = categoryId
                };
                _context.LicenseCategories.Add(licenseCategory);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return license.LicenseId;
        }
    }


}
