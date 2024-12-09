using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._OwnershipHistory
{
    public class GetAllOwnershipHistoriesQuery : IRequest<List<OwnershipHistoryDTO>>
    {
    }

}
