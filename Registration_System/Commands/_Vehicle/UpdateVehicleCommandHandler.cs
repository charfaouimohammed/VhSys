using MediatR;
using Registration_System.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Commands._Vehicle
{
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, bool>
    {
        private readonly RegistrationSystemDbContext _context;

        public UpdateVehicleCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _context.Vehicles.FindAsync(new object[] { request.VehicleId }, cancellationToken);

            if (vehicle == null)
                throw new KeyNotFoundException("Vehicle not found.");

            vehicle.Make = request.Make;
            vehicle.Model = request.Model;
            vehicle.Year = request.Year;
            vehicle.Color = request.Color;
            vehicle.RegistrationOfficeId = request.RegistrationOfficeId;
            vehicle.CurrentOwnerId = request.CurrentOwnerId;

            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
