using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.Models;

namespace Registration_System.Commands._TrafficViolation
{
    public class DeleteTrafficViolationCommandHandler : IRequestHandler<DeleteTrafficViolationCommand, bool>
    {
        private readonly RegistrationSystemDbContext _context;

        public DeleteTrafficViolationCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteTrafficViolationCommand request, CancellationToken cancellationToken)
        {
            var trafficViolation = await _context.TrafficViolations
                .FirstOrDefaultAsync(v => v.ViolationId == request.ViolationId, cancellationToken);

            if (trafficViolation == null)
            {
                return false;
            }

            _context.TrafficViolations.Remove(trafficViolation);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
