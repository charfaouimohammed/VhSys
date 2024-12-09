using MediatR;
using Registration_System.Models;

namespace Registration_System.Commands._Notification
{
    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, bool>
    {
        private readonly RegistrationSystemDbContext _context;

        public UpdateNotificationCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _context.Notifications.FindAsync(request.NotificationId);
            if (notification == null)
            {
                return false;
            }

            notification.OwnerId = request.OwnerId;
            notification.Message = request.Message;
            notification.Timestamp = request.Timestamp;
            notification.Status = request.Status;
            notification.IsDeleted = request.IsDeleted;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
