using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.Models;

namespace Registration_System.Commands._Vehicle
{
    public class TransferVehicleOwnershipCommandHandler : IRequestHandler<TransferVehicleOwnershipCommand, bool>
    {
        private readonly RegistrationSystemDbContext _context;

        public TransferVehicleOwnershipCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(TransferVehicleOwnershipCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.OwnershipHistories)
                .FirstOrDefaultAsync(v => v.VehicleId == request.VehicleId, cancellationToken);

            if (vehicle == null)
            {
                // Vehicle not found
                return false;
            }

            // Check if the new owner already owns the vehicle
            var existingOwnership = vehicle.OwnershipHistories
                .FirstOrDefault(oh => oh.OwnerId == request.NewOwnerId && oh.IsCurrentOwner);

            if (existingOwnership != null)
            {
                // New owner already owns the vehicle
                return false;
            }

            // Transfer ownership logic
            var currentOwnerHistory = vehicle.OwnershipHistories
                .FirstOrDefault(oh => oh.IsCurrentOwner);

            if (currentOwnerHistory != null)
            {
                currentOwnerHistory.IsCurrentOwner = false;
                currentOwnerHistory.EndDate = DateOnly.FromDateTime(DateTime.Now);
            }

            // Create a new ownership history for the new owner
            var newOwnershipHistory = new OwnershipHistory
            {
                OwnershipId = Guid.NewGuid(),
                VehicleId = request.VehicleId,
                OwnerId = request.NewOwnerId,
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                IsCurrentOwner = true
            };

            // Add new ownership history to the vehicle
            vehicle.OwnershipHistories.Add(newOwnershipHistory);

            // Update the vehicle record with the new ownership
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}