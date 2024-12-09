using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;

namespace Registration_System.Querys._Insurance
{
    public class GetAllInsurancesQueryHandler : IRequestHandler<GetAllInsurancesQuery, List<InsuranceDTO>>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetAllInsurancesQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<InsuranceDTO>> Handle(GetAllInsurancesQuery request, CancellationToken cancellationToken)
        {
            var insurances = await _context.Insurances
                .Select(i => new InsuranceDTO
                {
                    InsuranceId = i.InsuranceId,
                    VehicleId = i.VehicleId,
                    PolicyNumber = i.PolicyNumber,
                    StartDate = i.StartDate,
                    EndDate = i.EndDate,
                    Provider = i.Provider,
                    IsDeleted = i.IsDeleted
                })
                .ToListAsync(cancellationToken);

            return insurances;
        }
    }

}
