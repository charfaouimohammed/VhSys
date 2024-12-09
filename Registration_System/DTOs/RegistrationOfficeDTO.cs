namespace Registration_System.DTOs
{
    public class RegistrationOfficeDTO
    {
        public Guid OfficeId { get; set; }
        public string? OfficeName { get; set; }
        public string? Address { get; set; }
        public Guid? AdminId { get; set; }
        public bool? IsDeleted { get; set; }
    }

}
