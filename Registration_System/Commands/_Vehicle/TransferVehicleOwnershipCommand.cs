using MediatR;

namespace Registration_System.Commands._Vehicle
{
    public class TransferVehicleOwnershipCommand : IRequest<bool>
    {
        public Guid VehicleId { get; set; }
        public Guid NewOwnerId { get; set; }
    }
}
