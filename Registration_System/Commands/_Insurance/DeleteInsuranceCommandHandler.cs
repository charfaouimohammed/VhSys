using MediatR;
using Registration_System.Models;

namespace Registration_System.Commands._Insurance
{
    public class DeleteInsuranceCommandHandler : IRequestHandler<DeleteInsuranceCommand, bool>
    {
        private readonly RegistrationSystemDbContext _context;

        public DeleteInsuranceCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteInsuranceCommand request, CancellationToken cancellationToken)
        {
            var insurance = await _context.Insurances.FindAsync(request.InsuranceId);
            if (insurance == null)
            {
                return false;
            }

            _context.Insurances.Remove(insurance);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
