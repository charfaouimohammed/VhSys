using MediatR;

namespace Registration_System.Commands._AuditLog
{
    public class CreateAuditLogCommand : IRequest<Guid>
    {
        public Guid AdminId { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
    }

}
