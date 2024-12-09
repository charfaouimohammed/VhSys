namespace Registration_System.DTOs
{
    public class AuditLogDTO
    {
        public Guid LogId { get; set; }
        public Guid? AdminId { get; set; }
        public string Action { get; set; }
        public DateTime? Timestamp { get; set; }
        public bool? IsDeleted { get; set; }
    }

}
