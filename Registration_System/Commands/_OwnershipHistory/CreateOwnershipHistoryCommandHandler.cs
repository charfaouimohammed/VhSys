using MediatR;
using Registration_System.Models;

namespace Registration_System.Commands._OwnershipHistory
{
    public class CreateOwnershipHistoryCommandHandler : IRequestHandler<CreateOwnershipHistoryCommand, Guid>
    {
        private readonly RegistrationSystemDbContext _context;

        public CreateOwnershipHistoryCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateOwnershipHistoryCommand request, CancellationToken cancellationToken)
        {
            var ownershipHistory = new OwnershipHistory
            {
                VehicleId = request.VehicleId,
                OwnerId = request.OwnerId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsCurrentOwner = request.IsCurrentOwner
            };

            _context.OwnershipHistories.Add(ownershipHistory);
            await _context.SaveChangesAsync(cancellationToken);

            return ownershipHistory.OwnershipId;
        }
    }

}
