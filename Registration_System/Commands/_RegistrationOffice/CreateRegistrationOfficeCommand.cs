using MediatR;

namespace Registration_System.Commands._RegistrationOffice
{
    public class CreateRegistrationOfficeCommand : IRequest<Guid>
    {
        public string? OfficeName { get; set; }
        public string? Address { get; set; }
        public Guid? AdminId { get; set; }
    }

}
