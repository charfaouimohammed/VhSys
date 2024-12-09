using MediatR;

namespace Registration_System.Commands._AuditLog
{
    public class DeleteAuditLogCommand : IRequest<bool>
    {
        public Guid LogId { get; set; }
    }

}
