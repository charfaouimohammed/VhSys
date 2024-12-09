using MediatR;

namespace Registration_System.Commands._AuditLog
{
    public class UpdateAuditLogCommand : IRequest<bool>
    {
        public Guid LogId { get; set; }
        public Guid? AdminId { get; set; }
        public string Action { get; set; }
        public DateTime? Timestamp { get; set; }
        public bool? IsDeleted { get; set; }
    }

}
