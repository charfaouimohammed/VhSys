using MediatR;

namespace Registration_System.Commands._DriverLicense
{
    public class UpdateDriverLicenseCommand : IRequest<Unit>
    {
        public Guid LicenseId { get; set; }
        public string LicenseNumber { get; set; }
        public DateOnly IssueDate { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public List<int> CategoryIds { get; set; }
    }

}
