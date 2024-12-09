namespace Registration_System.DTOs
{
    public class NotificationDTO
    {
        public Guid NotificationId { get; set; }
        public Guid? OwnerId { get; set; }
        public string? Message { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? Status { get; set; }
        public bool? IsDeleted { get; set; }
    }

}
