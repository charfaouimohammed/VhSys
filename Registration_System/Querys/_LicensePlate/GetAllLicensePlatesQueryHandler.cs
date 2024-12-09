using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;

namespace Registration_System.Querys._LicensePlate
{
    public class GetAllLicensePlatesQueryHandler : IRequestHandler<GetAllLicensePlatesQuery, List<LicensePlateDTO>>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetAllLicensePlatesQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<LicensePlateDTO>> Handle(GetAllLicensePlatesQuery request, CancellationToken cancellationToken)
        {
            var licensePlates = await _context.LicensePlates
                .Select(lp => new LicensePlateDTO
                {
                    LicensePlateId = lp.LicensePlateId,
                    VehicleId = lp.VehicleId,
                    PlateNumber = lp.PlateNumber,
                    IssueDate = lp.IssueDate,
                    ExpiryDate = lp.ExpiryDate,
                    IsDeleted = lp.IsDeleted
                })
                .ToListAsync(cancellationToken);

            return licensePlates;
        }
    }

}
