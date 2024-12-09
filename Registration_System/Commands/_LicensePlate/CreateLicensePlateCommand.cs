using MediatR;

namespace Registration_System.Commands._LicensePlate
{
    public class CreateLicensePlateCommand : IRequest<Guid>
    {
        public Guid? VehicleId { get; set; }
        public string? PlateNumber { get; set; }
        public DateOnly? IssueDate { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        public bool? IsDeleted { get; set; }
    }

}
