using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;

namespace Registration_System.Querys._TrafficViolation
{
    public class GetTrafficViolationQueryHandler : IRequestHandler<GetTrafficViolationQuery, TrafficViolationDTO>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetTrafficViolationQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<TrafficViolationDTO> Handle(GetTrafficViolationQuery request, CancellationToken cancellationToken)
        {
            var trafficViolation = await _context.TrafficViolations
                .Where(v => v.ViolationId == request.ViolationId)
                .FirstOrDefaultAsync(cancellationToken);

            if (trafficViolation == null)
            {
                return null;
            }

            return new TrafficViolationDTO
            {
                ViolationId = trafficViolation.ViolationId,
                LicensePlateId = trafficViolation.LicensePlateId,
                DriverLicenseId = trafficViolation.DriverLicenseId,
                ViolationType = trafficViolation.ViolationType,
                Date = trafficViolation.Date,
                FineAmount = trafficViolation.FineAmount,
                PaymentStatus = trafficViolation.PaymentStatus,
                IsDeleted = trafficViolation.IsDeleted
            };
        }
    }

}
