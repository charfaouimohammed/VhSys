using MediatR;

namespace Registration_System.Commands._TrafficViolation
{
    public class UpdateTrafficViolationCommand : IRequest<bool>
    {
        public Guid ViolationId { get; set; }
        public Guid? LicensePlateId { get; set; }
        public Guid? DriverLicenseId { get; set; }
        public string? ViolationType { get; set; }
        public DateOnly? Date { get; set; }
        public decimal? FineAmount { get; set; }
        public string? PaymentStatus { get; set; }
    }

}
