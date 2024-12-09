using MediatR;
using Registration_System.DTOs;
using Registration_System.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Commands._Owner
{
    public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, OwnerDto>
    {
        private readonly RegistrationSystemDbContext _context;

        public CreateOwnerCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<OwnerDto> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            // Create a new Owner object based on the provided data
            var owner = new Owner
            {
                OwnerId = Guid.NewGuid(),  // Generate a new unique GUID for the OwnerId
                Cin = request.Cin,  // Set the CIN
                Name = request.Name,  // Set the Name
                DateOfBirth = request.DateOfBirth,  // Set the DateOfBirth
                Address = request.Address,  // Set the Address
                PhoneNumber = request.PhoneNumber,  // Set the PhoneNumber
                Email = request.Email,  // Set the Email
                //IsDeleted = request.IsDeleted  // Set the IsDeleted flag
            };

            // Add the new owner to the context
            _context.Owners.Add(owner);
            await _context.SaveChangesAsync(cancellationToken);  // Save changes to the database

            // Return the created owner as a DTO
            return new OwnerDto
            {
                OwnerId = owner.OwnerId,
                Cin = owner.Cin,
                Name = owner.Name,
                DateOfBirth = owner.DateOfBirth,
                Address = owner.Address,
                PhoneNumber = owner.PhoneNumber,
                Email = owner.Email,
                //IsDeleted = owner.IsDeleted // Include the IsDeleted status
            };
        }
    }
}
