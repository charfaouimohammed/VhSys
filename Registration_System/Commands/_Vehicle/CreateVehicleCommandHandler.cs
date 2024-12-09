using MediatR;
using Registration_System.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Commands._Vehicle
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Guid>
    {
        private readonly RegistrationSystemDbContext _context;

        public CreateVehicleCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                // Create a new Vehicle
                var vehicle = new Vehicle
                {
                    VehicleId = Guid.NewGuid(),
                    Make = request.Make,
                    Model = request.Model,
                    Year = request.Year,
                    Color = request.Color,
                    RegistrationOfficeId = request.RegistrationOfficeId,
                    CurrentOwnerId = request.CurrentOwnerId,
                    IsDeleted = false
                };

                _context.Vehicles.Add(vehicle);

                // Generate a License Plate
                var licensePlate = new LicensePlate
                {
                    LicensePlateId = Guid.NewGuid(),
                    VehicleId = vehicle.VehicleId,
                    PlateNumber = GenerateLicensePlateNumber(),
                    IssueDate = DateOnly.FromDateTime(DateTime.Now),
                    ExpiryDate = DateOnly.FromDateTime(DateTime.Now.AddYears(15)),
                    IsDeleted = false
                };

                _context.LicensePlates.Add(licensePlate);

                // Save to database
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return vehicle.VehicleId;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception($"Error creating vehicle: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
        }

        private string GenerateLicensePlateNumber()
        {
            var random = new Random();
            return $"{random.Next(1000, 9999)}-{(char)random.Next(65, 91)}-{random.Next(10, 99)}";
        }
    }
}
