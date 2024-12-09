using MediatR;
using Registration_System.Models;

namespace Registration_System.Commands._Insurance
{
    public class CreateInsuranceCommandHandler : IRequestHandler<CreateInsuranceCommand, Guid>
    {
        private readonly RegistrationSystemDbContext _context;

        public CreateInsuranceCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateInsuranceCommand request, CancellationToken cancellationToken)
        {
            var insurance = new Insurance
            {
                VehicleId = request.VehicleId,
                PolicyNumber = request.PolicyNumber,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Provider = request.Provider,
                IsDeleted = request.IsDeleted
            };

            _context.Insurances.Add(insurance);
            await _context.SaveChangesAsync(cancellationToken);

            return insurance.InsuranceId;
        }
    }

}
