using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;

namespace Registration_System.Querys._Notification
{
    public class GetNotificationQueryHandler : IRequestHandler<GetNotificationQuery, NotificationDTO>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetNotificationQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<NotificationDTO> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
        {
            var notification = await _context.Notifications
                .Where(n => n.NotificationId == request.NotificationId)
                .FirstOrDefaultAsync(cancellationToken);

            if (notification == null)
            {
                return null;
            }

            return new NotificationDTO
            {
                NotificationId = notification.NotificationId,
                OwnerId = notification.OwnerId,
                Message = notification.Message,
                Timestamp = notification.Timestamp,
                Status = notification.Status,
                IsDeleted = notification.IsDeleted
            };
        }
    }

}
