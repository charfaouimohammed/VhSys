using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._OwnershipHistory
{
    public class GetOwnershipHistoryQuery : IRequest<OwnershipHistoryDTO>
    {
        public Guid OwnershipId { get; set; }
    }

}
