using MediatR;
using Registration_System.Models;

namespace Registration_System.Commands._AuditLog
{
    public class UpdateAuditLogCommandHandler : IRequestHandler<UpdateAuditLogCommand, bool>
    {
        private readonly RegistrationSystemDbContext _context;

        public UpdateAuditLogCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateAuditLogCommand request, CancellationToken cancellationToken)
        {
            var auditLog = await _context.AuditLogs.FindAsync(request.LogId);
            if (auditLog == null)
            {
                return false;
            }

            auditLog.AdminId = request.AdminId ?? auditLog.AdminId;
            auditLog.Action = request.Action ?? auditLog.Action;
            auditLog.Timestamp = request.Timestamp ?? auditLog.Timestamp;
            auditLog.IsDeleted = request.IsDeleted ?? auditLog.IsDeleted;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
