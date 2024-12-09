using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._Administration
{
    public class GetAllAdministrationsQuery : IRequest<List<AdministrationDTO>>
    {
    }

}
