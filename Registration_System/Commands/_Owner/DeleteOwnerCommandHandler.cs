using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Commands._Owner
{
    public class DeleteOwnerCommandHandler : IRequestHandler<DeleteOwnerCommand, bool>
    {
        private readonly RegistrationSystemDbContext _context;

        public DeleteOwnerCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
        {
            // Find the owner by the provided ID
            var owner = await _context.Owners
                .FirstOrDefaultAsync(o => o.OwnerId == request.OwnerId && o.IsDeleted != true, cancellationToken);

            if (owner == null)
            {
                // If the owner is not found or already deleted, return false
                return false;
            }

            // Mark the owner as deleted
            owner.IsDeleted = true;

            // Update the owner entity
            _context.Owners.Update(owner);

            // Save the changes to the database
            await _context.SaveChangesAsync(cancellationToken);

            // Return true if the deletion was successful
            return true;
        }
    }

}
