using MediatR;

namespace Registration_System.Commands._RegistrationOffice
{
    public class UpdateRegistrationOfficeCommand : IRequest<bool>
    {
        public Guid OfficeId { get; set; }
        public string? OfficeName { get; set; }
        public string? Address { get; set; }
        public Guid? AdminId { get; set; }
    }

}
