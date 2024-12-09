using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.Models;

namespace Registration_System.Commands._OwnershipHistory
{
    public class DeleteOwnershipHistoryCommandHandler : IRequestHandler<DeleteOwnershipHistoryCommand, bool>
    {
        private readonly RegistrationSystemDbContext _context;

        public DeleteOwnershipHistoryCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteOwnershipHistoryCommand request, CancellationToken cancellationToken)
        {
            var ownershipHistory = await _context.OwnershipHistories
                .FirstOrDefaultAsync(o => o.OwnershipId == request.OwnershipId, cancellationToken);

            if (ownershipHistory == null)
            {
                return false;
            }

            _context.OwnershipHistories.Remove(ownershipHistory);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
