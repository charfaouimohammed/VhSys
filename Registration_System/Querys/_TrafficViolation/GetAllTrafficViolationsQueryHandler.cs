using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;

namespace Registration_System.Querys._TrafficViolation
{
    public class GetAllTrafficViolationsQueryHandler : IRequestHandler<GetAllTrafficViolationsQuery, List<TrafficViolationDTO>>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetAllTrafficViolationsQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<TrafficViolationDTO>> Handle(GetAllTrafficViolationsQuery request, CancellationToken cancellationToken)
        {
            var trafficViolations = await _context.TrafficViolations
                .Select(v => new TrafficViolationDTO
                {
                    ViolationId = v.ViolationId,
                    LicensePlateId = v.LicensePlateId,
                    DriverLicenseId = v.DriverLicenseId,
                    ViolationType = v.ViolationType,
                    Date = v.Date,
                    FineAmount = v.FineAmount,
                    PaymentStatus = v.PaymentStatus,
                    IsDeleted = v.IsDeleted
                })
                .ToListAsync(cancellationToken);

            return trafficViolations;
        }
    }

}
