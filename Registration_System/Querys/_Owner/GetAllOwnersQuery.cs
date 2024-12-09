using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._Owner
{
    public class GetAllOwnersQuery : IRequest<List<OwnerDto>>
    {
    }

}
