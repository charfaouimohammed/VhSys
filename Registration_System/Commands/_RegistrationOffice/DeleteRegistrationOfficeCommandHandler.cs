using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.Models;

namespace Registration_System.Commands._RegistrationOffice
{
    public class DeleteRegistrationOfficeCommandHandler : IRequestHandler<DeleteRegistrationOfficeCommand, bool>
    {
        private readonly RegistrationSystemDbContext _context;

        public DeleteRegistrationOfficeCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteRegistrationOfficeCommand request, CancellationToken cancellationToken)
        {
            var registrationOffice = await _context.RegistrationOffices
                .FirstOrDefaultAsync(o => o.OfficeId == request.OfficeId, cancellationToken);

            if (registrationOffice == null)
            {
                return false;
            }

            _context.RegistrationOffices.Remove(registrationOffice);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
