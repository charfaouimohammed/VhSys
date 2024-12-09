using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.Models;

namespace Registration_System.Commands._RegistrationOffice
{
    public class UpdateRegistrationOfficeCommandHandler : IRequestHandler<UpdateRegistrationOfficeCommand, bool>
    {
        private readonly RegistrationSystemDbContext _context;

        public UpdateRegistrationOfficeCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateRegistrationOfficeCommand request, CancellationToken cancellationToken)
        {
            var registrationOffice = await _context.RegistrationOffices
                .FirstOrDefaultAsync(o => o.OfficeId == request.OfficeId, cancellationToken);

            if (registrationOffice == null)
            {
                return false;
            }

            registrationOffice.OfficeName = request.OfficeName;
            registrationOffice.Address = request.Address;
            registrationOffice.AdminId = request.AdminId;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
