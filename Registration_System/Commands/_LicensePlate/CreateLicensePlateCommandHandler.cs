using MediatR;
using Registration_System.Models;

namespace Registration_System.Commands._LicensePlate
{
    public class CreateLicensePlateCommandHandler : IRequestHandler<CreateLicensePlateCommand, Guid>
    {
        private readonly RegistrationSystemDbContext _context;

        public CreateLicensePlateCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateLicensePlateCommand request, CancellationToken cancellationToken)
        {
            var licensePlate = new LicensePlate
            {
                VehicleId = request.VehicleId,
                PlateNumber = request.PlateNumber,
                IssueDate = request.IssueDate,
                ExpiryDate = request.ExpiryDate,
                IsDeleted = request.IsDeleted
            };

            _context.LicensePlates.Add(licensePlate);
            await _context.SaveChangesAsync(cancellationToken);

            return licensePlate.LicensePlateId;
        }
    }

}
