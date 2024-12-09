using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.Models;

namespace Registration_System.Commands._OwnershipHistory
{
    public class UpdateOwnershipHistoryCommandHandler : IRequestHandler<UpdateOwnershipHistoryCommand, bool>
    {
        private readonly RegistrationSystemDbContext _context;

        public UpdateOwnershipHistoryCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateOwnershipHistoryCommand request, CancellationToken cancellationToken)
        {
            var ownershipHistory = await _context.OwnershipHistories
                .FirstOrDefaultAsync(o => o.OwnershipId == request.OwnershipId, cancellationToken);

            if (ownershipHistory == null)
            {
                return false;
            }

            ownershipHistory.VehicleId = request.VehicleId;
            ownershipHistory.OwnerId = request.OwnerId;
            ownershipHistory.StartDate = request.StartDate;
            ownershipHistory.EndDate = request.EndDate;
            ownershipHistory.IsCurrentOwner = request.IsCurrentOwner;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
