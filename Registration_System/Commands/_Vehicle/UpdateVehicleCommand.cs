using MediatR;
using System;

namespace Registration_System.Commands._Vehicle
{
    public class UpdateVehicleCommand : IRequest<bool>
    {
        public Guid VehicleId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public Guid RegistrationOfficeId { get; set; }
        public Guid CurrentOwnerId { get; set; }
    }
}
