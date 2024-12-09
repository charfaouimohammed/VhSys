using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;

namespace Registration_System.Querys._Insurance
{
    public class GetInsuranceQueryHandler : IRequestHandler<GetInsuranceQuery, InsuranceDTO>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetInsuranceQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<InsuranceDTO> Handle(GetInsuranceQuery request, CancellationToken cancellationToken)
        {
            var insurance = await _context.Insurances
                .Where(i => i.InsuranceId == request.InsuranceId)
                .FirstOrDefaultAsync(cancellationToken);

            if (insurance == null)
            {
                return null;
            }

            return new InsuranceDTO
            {
                InsuranceId = insurance.InsuranceId,
                VehicleId = insurance.VehicleId,
                PolicyNumber = insurance.PolicyNumber,
                StartDate = insurance.StartDate,
                EndDate = insurance.EndDate,
                Provider = insurance.Provider,
                IsDeleted = insurance.IsDeleted
            };
        }
    }

}
