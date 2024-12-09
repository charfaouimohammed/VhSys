

namespace Registration_System.DTOs
{
    public class OwnerDto
    {
        public Guid OwnerId { get; set; }
        public string? Cin { get; set; }
        public string? Name { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public List<DriverLicenseDTO> DriverLicenses { get; set; } = new List<DriverLicenseDTO>();
        public List<NotificationDTO> Notifications { get; set; } = new List<NotificationDTO>();
        public List<OwnershipHistoryDTO> OwnershipHistories { get; set; } = new List<OwnershipHistoryDTO>();
        public List<VehicleDTO> Vehicles { get; set; } = new List<VehicleDTO>();
    }
}
