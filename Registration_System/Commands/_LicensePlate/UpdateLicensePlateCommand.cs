using MediatR;

namespace Registration_System.Commands._LicensePlate
{
    public class UpdateLicensePlateCommand : IRequest<bool>
    {
        public Guid LicensePlateId { get; set; }
        public Guid? VehicleId { get; set; }
        public string? PlateNumber { get; set; }
        public DateOnly? IssueDate { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        public bool? IsDeleted { get; set; }
    }

}
