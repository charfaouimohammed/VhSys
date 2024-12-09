using MediatR;
using Registration_System.Models;

namespace Registration_System.Commands._AuditLog
{
    public class DeleteAuditLogCommandHandler : IRequestHandler<DeleteAuditLogCommand, bool>
    {
        private readonly RegistrationSystemDbContext _context;

        public DeleteAuditLogCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteAuditLogCommand request, CancellationToken cancellationToken)
        {
            var auditLog = await _context.AuditLogs.FindAsync(request.LogId);
            if (auditLog == null)
            {
                return false;
            }

            auditLog.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
