using MediatR;

namespace Registration_System.Commands._OwnershipHistory
{
    public class DeleteOwnershipHistoryCommand : IRequest<bool>
    {
        public Guid OwnershipId { get; set; }
    }

}
