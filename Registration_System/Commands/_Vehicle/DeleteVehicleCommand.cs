using MediatR;
using System;

namespace Registration_System.Commands._Vehicle
{
    public class DeleteVehicleCommand : IRequest<bool>
    {
        public Guid VehicleId { get; set; }
    }
}
