namespace Registration_System.DTOs
{
    public class DriverLicenseDTO
    {
        public Guid LicenseId { get; set; }
        public Guid OwnerId { get; set; }
        public string LicenseNumber { get; set; }
        public DateOnly IssueDate { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
    }

}
