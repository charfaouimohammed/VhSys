using MediatR;
using Registration_System.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Commands._Vehicle
{
    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, bool>
    {
        private readonly RegistrationSystemDbContext _context;

        public DeleteVehicleCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            // Find the vehicle by its ID
            var vehicle = await _context.Vehicles.FindAsync(new object[] { request.VehicleId }, cancellationToken);

            if (vehicle == null)
                throw new KeyNotFoundException("Vehicle not found.");

            // Soft delete the vehicle by marking it as deleted
            vehicle.IsDeleted = true;

            // Find and soft delete the associated license plate
            var licensePlate = _context.LicensePlates.FirstOrDefault(lp => lp.VehicleId == request.VehicleId);

            if (licensePlate != null)
            {
                licensePlate.IsDeleted = true;
                _context.LicensePlates.Update(licensePlate);
            }

            // Update the vehicle in the database
            _context.Vehicles.Update(vehicle);

            // Save the changes to the database
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
