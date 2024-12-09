using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Commands._Owner
{
    public class CreateOwnerCommand : IRequest<OwnerDto>
    {
        public string Cin { get; set; }  // CIN
        public string Name { get; set; }  // Name
        public DateOnly DateOfBirth { get; set; }  // DateOfBirth
        public string Address { get; set; }  // Address
        public string PhoneNumber { get; set; }  // PhoneNumber
        public string Email { get; set; }  // Email
        //public bool IsDeleted { get; set; } = false; // IsDeleted (soft delete)
    }

}
