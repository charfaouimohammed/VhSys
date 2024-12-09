using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;

namespace Registration_System.Querys._AuditLog
{
    public class GetAllAuditLogsQueryHandler : IRequestHandler<GetAllAuditLogsQuery, List<AuditLogDTO>>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetAllAuditLogsQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<AuditLogDTO>> Handle(GetAllAuditLogsQuery request, CancellationToken cancellationToken)
        {
            var auditLogs = await _context.AuditLogs
                .Select(auditLog => new AuditLogDTO
                {
                    LogId = auditLog.LogId,
                    AdminId = auditLog.AdminId,
                    Action = auditLog.Action,
                    Timestamp = auditLog.Timestamp,
                    IsDeleted = auditLog.IsDeleted
                })
                .ToListAsync(cancellationToken);

            return auditLogs;
        }
    }

}
