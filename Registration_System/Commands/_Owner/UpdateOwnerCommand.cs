using MediatR;
using Registration_System.DTOs;
using System;

namespace Registration_System.Commands._Owner
{
    public class UpdateOwnerCommand : IRequest<Unit>
    {
        public Guid OwnerId { get;set; }
        public string? Cin { get; set; }
        public string? Name { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        //public bool? IsDeleted { get; set; }
        
    }
}
