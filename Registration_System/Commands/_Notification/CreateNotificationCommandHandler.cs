using MediatR;
using Registration_System.Models;

namespace Registration_System.Commands._Notification
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, Guid>
    {
        private readonly RegistrationSystemDbContext _context;

        public CreateNotificationCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = new Notification
            {
                OwnerId = request.OwnerId,
                Message = request.Message,
                Timestamp = request.Timestamp,
                Status = request.Status,
                IsDeleted = request.IsDeleted
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync(cancellationToken);

            return notification.NotificationId;
        }
    }

}
