using MediatR;
using Registration_System.Models;

namespace Registration_System.Commands._LicensePlate
{
    public class DeleteLicensePlateCommandHandler : IRequestHandler<DeleteLicensePlateCommand, bool>
    {
        private readonly RegistrationSystemDbContext _context;

        public DeleteLicensePlateCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteLicensePlateCommand request, CancellationToken cancellationToken)
        {
            var licensePlate = await _context.LicensePlates.FindAsync(request.LicensePlateId);
            if (licensePlate == null)
            {
                return false;
            }

            _context.LicensePlates.Remove(licensePlate);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
