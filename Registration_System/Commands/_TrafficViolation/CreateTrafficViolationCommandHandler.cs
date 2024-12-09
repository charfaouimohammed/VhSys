using MediatR;
using Registration_System.Models;

namespace Registration_System.Commands._TrafficViolation
{
    public class CreateTrafficViolationCommandHandler : IRequestHandler<CreateTrafficViolationCommand, Guid>
    {
        private readonly RegistrationSystemDbContext _context;

        public CreateTrafficViolationCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateTrafficViolationCommand request, CancellationToken cancellationToken)
        {
            var trafficViolation = new TrafficViolation
            {
                ViolationId = Guid.NewGuid(),  // Ensure ViolationId is set to a valid GUID
                LicensePlateId = request.LicensePlateId,
                DriverLicenseId = request.DriverLicenseId,
                ViolationType = request.ViolationType,
                Date = request.Date,
                FineAmount = request.FineAmount,
                PaymentStatus = request.PaymentStatus
            };

            _context.TrafficViolations.Add(trafficViolation);
            await _context.SaveChangesAsync(cancellationToken);

            return trafficViolation.ViolationId;
        }

    }

}
