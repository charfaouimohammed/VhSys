using MediatR;

namespace Registration_System.Commands._OwnershipHistory
{
    public class UpdateOwnershipHistoryCommand : IRequest<bool>
    {
        public Guid OwnershipId { get; set; }
        public Guid? VehicleId { get; set; }
        public Guid? OwnerId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool IsCurrentOwner { get; set; }
    }

}
