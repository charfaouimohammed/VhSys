using MediatR;
using Registration_System.Models;

namespace Registration_System.Commands._Insurance
{
    public class UpdateInsuranceCommandHandler : IRequestHandler<UpdateInsuranceCommand, bool>
    {
        private readonly RegistrationSystemDbContext _context;

        public UpdateInsuranceCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateInsuranceCommand request, CancellationToken cancellationToken)
        {
            var insurance = await _context.Insurances.FindAsync(request.InsuranceId);
            if (insurance == null)
            {
                return false;
            }

            insurance.VehicleId = request.VehicleId;
            insurance.PolicyNumber = request.PolicyNumber;
            insurance.StartDate = request.StartDate;
            insurance.EndDate = request.EndDate;
            insurance.Provider = request.Provider;
            insurance.IsDeleted = request.IsDeleted;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
