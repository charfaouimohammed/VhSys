using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._RegistrationOffice
{
    public class GetAllRegistrationOfficesQuery : IRequest<List<RegistrationOfficeDTO>>
    {
    }

}
