using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Commands._DriverLicense
{
    public class DeleteDriverLicenseCommandHandler : IRequestHandler<DeleteDriverLicenseCommand, Unit>
    {
        private readonly RegistrationSystemDbContext _context;

        public DeleteDriverLicenseCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteDriverLicenseCommand request, CancellationToken cancellationToken)
        {
            var license = await _context.DriverLicenses
                .FirstOrDefaultAsync(dl => dl.LicenseId == request.LicenseId, cancellationToken);

            if (license == null)
            {
                throw new Exception("Driver License not found");
            }

            _context.DriverLicenses.Remove(license);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
