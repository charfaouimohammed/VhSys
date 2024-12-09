using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.Models;

namespace Registration_System.Commands._TrafficViolation
{
    public class UpdateTrafficViolationCommandHandler : IRequestHandler<UpdateTrafficViolationCommand, bool>
    {
        private readonly RegistrationSystemDbContext _context;

        public UpdateTrafficViolationCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateTrafficViolationCommand request, CancellationToken cancellationToken)
        {
            var trafficViolation = await _context.TrafficViolations
                .FirstOrDefaultAsync(v => v.ViolationId == request.ViolationId, cancellationToken);

            if (trafficViolation == null)
            {
                return false;
            }

            trafficViolation.LicensePlateId = request.LicensePlateId;
            trafficViolation.DriverLicenseId = request.DriverLicenseId;
            trafficViolation.ViolationType = request.ViolationType;
            trafficViolation.Date = request.Date;
            trafficViolation.FineAmount = request.FineAmount;
            trafficViolation.PaymentStatus = request.PaymentStatus;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
