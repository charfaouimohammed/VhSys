using MediatR;
using Registration_System.DTOs;
using Registration_System.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Commands._Owner
{
    public class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand, Unit>
    {
        private readonly RegistrationSystemDbContext _context;

        public UpdateOwnerCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
        {
            // Find the existing owner by CIN (or another identifier)
            var owner = await _context.Owners.FindAsync(request.OwnerId);

            if (owner == null || owner.IsDeleted == true)
            {
                // If the owner is not found or is deleted, throw an exception or return a specific response
                throw new Exception("Owner not found or is deleted");
            }

            // Update the owner's properties
            owner.Name = request.Name ?? owner.Name;
            owner.DateOfBirth = request.DateOfBirth ?? owner.DateOfBirth;
            owner.Address = request.Address ?? owner.Address;
            owner.PhoneNumber = request.PhoneNumber ?? owner.PhoneNumber;
            owner.Email = request.Email ?? owner.Email;
            //owner.IsDeleted = request.IsDeleted ?? owner.IsDeleted;

            // Save changes to the database
            await _context.SaveChangesAsync(cancellationToken);

            // Return Unit to indicate completion
            return Unit.Value;
        }
    }
}
