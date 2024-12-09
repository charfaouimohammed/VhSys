using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;

namespace Registration_System.Querys._AuditLog
{
    public class GetAuditLogQueryHandler : IRequestHandler<GetAuditLogQuery, AuditLogDTO>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetAuditLogQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<AuditLogDTO> Handle(GetAuditLogQuery request, CancellationToken cancellationToken)
        {
            var auditLog = await _context.AuditLogs
                .Where(a => a.LogId == request.LogId)
                .FirstOrDefaultAsync(cancellationToken);

            if (auditLog == null)
            {
                return null;
            }

            return new AuditLogDTO
            {
                LogId = auditLog.LogId,
                AdminId = auditLog.AdminId,
                Action = auditLog.Action,
                Timestamp = auditLog.Timestamp,
                IsDeleted = auditLog.IsDeleted
            };
        }
    }

}
