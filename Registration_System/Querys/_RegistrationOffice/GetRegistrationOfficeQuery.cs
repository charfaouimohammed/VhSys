using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._RegistrationOffice
{
    public class GetRegistrationOfficeQuery : IRequest<RegistrationOfficeDTO>
    {
        public Guid OfficeId { get; set; }
    }

}
