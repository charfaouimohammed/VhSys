using Registration_System.Models;

namespace Registration_System.DTOs
{
    public class LicensePlateDTO
    {
        public Guid LicensePlateId { get; set; }
        public Guid? VehicleId { get; set; }
        public string? PlateNumber { get; set; }
        public DateOnly? IssueDate { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        public bool? IsDeleted { get; set; }

        // Navigation property back to Vehicle
        public virtual Vehicle Vehicle { get; set; }

    }

}
