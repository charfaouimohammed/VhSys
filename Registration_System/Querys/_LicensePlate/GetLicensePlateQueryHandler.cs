using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;

namespace Registration_System.Querys._LicensePlate
{
    public class GetLicensePlateQueryHandler : IRequestHandler<GetLicensePlateQuery, LicensePlateDTO>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetLicensePlateQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<LicensePlateDTO> Handle(GetLicensePlateQuery request, CancellationToken cancellationToken)
        {
            var licensePlate = await _context.LicensePlates
                .Where(lp => lp.LicensePlateId == request.LicensePlateId)
                .FirstOrDefaultAsync(cancellationToken);

            if (licensePlate == null)
            {
                return null;
            }

            return new LicensePlateDTO
            {
                LicensePlateId = licensePlate.LicensePlateId,
                VehicleId = licensePlate.VehicleId,
                PlateNumber = licensePlate.PlateNumber,
                IssueDate = licensePlate.IssueDate,
                ExpiryDate = licensePlate.ExpiryDate,
                IsDeleted = licensePlate.IsDeleted
            };
        }
    }

}
