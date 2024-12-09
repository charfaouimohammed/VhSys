using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._AuditLog
{
    public class GetAuditLogQuery : IRequest<AuditLogDTO>
    {
        public Guid LogId { get; set; }
    }

}
