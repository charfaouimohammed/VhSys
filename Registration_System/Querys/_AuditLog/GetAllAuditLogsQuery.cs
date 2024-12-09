using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._AuditLog
{
    public class GetAllAuditLogsQuery : IRequest<List<AuditLogDTO>>
    {
    }

}
