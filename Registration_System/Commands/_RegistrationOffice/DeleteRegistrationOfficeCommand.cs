using MediatR;

namespace Registration_System.Commands._RegistrationOffice
{
    public class DeleteRegistrationOfficeCommand : IRequest<bool>
    {
        public Guid OfficeId { get; set; }
    }

}
