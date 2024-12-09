using MediatR;
using Registration_System.Models;

namespace Registration_System.Commands._LicensePlate
{
    public class UpdateLicensePlateCommandHandler : IRequestHandler<UpdateLicensePlateCommand, bool>
    {
        private readonly RegistrationSystemDbContext _context;

        public UpdateLicensePlateCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateLicensePlateCommand request, CancellationToken cancellationToken)
        {
            var licensePlate = await _context.LicensePlates.FindAsync(request.LicensePlateId);
            if (licensePlate == null)
            {
                return false;
            }

            licensePlate.VehicleId = request.VehicleId;
            licensePlate.PlateNumber = request.PlateNumber;
            licensePlate.IssueDate = request.IssueDate;
            licensePlate.ExpiryDate = request.ExpiryDate;
            licensePlate.IsDeleted = request.IsDeleted;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
