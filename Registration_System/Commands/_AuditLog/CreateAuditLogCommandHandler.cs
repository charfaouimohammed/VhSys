using MediatR;
using Registration_System.Models;

namespace Registration_System.Commands._AuditLog
{
    public class CreateAuditLogCommandHandler : IRequestHandler<CreateAuditLogCommand, Guid>
    {
        private readonly RegistrationSystemDbContext _context;

        public CreateAuditLogCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateAuditLogCommand request, CancellationToken cancellationToken)
        {
            var auditLog = new AuditLog
            {
                LogId = Guid.NewGuid(),
                AdminId = request.AdminId,
                Action = request.Action,
                Timestamp = request.Timestamp,
                IsDeleted = false
            };

            _context.AuditLogs.Add(auditLog);
            await _context.SaveChangesAsync(cancellationToken);
            return auditLog.LogId;
        }
    }

}
