using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;

namespace Registration_System.Querys._Notification
{
    public class GetAllNotificationsQueryHandler : IRequestHandler<GetAllNotificationsQuery, List<NotificationDTO>>
{
    private readonly RegistrationSystemDbContext _context;

    public GetAllNotificationsQueryHandler(RegistrationSystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<NotificationDTO>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
    {
        var notifications = await _context.Notifications
            .Select(n => new NotificationDTO
            {
                NotificationId = n.NotificationId,
                OwnerId = n.OwnerId,
                Message = n.Message,
                Timestamp = n.Timestamp,
                Status = n.Status,
                IsDeleted = n.IsDeleted
            })
            .ToListAsync(cancellationToken);

        return notifications;
    }
}

}
