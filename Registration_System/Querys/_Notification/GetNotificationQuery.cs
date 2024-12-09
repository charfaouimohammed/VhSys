using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._Notification
{
    public class GetNotificationQuery : IRequest<NotificationDTO>
    {
        public Guid NotificationId { get; set; }
    }

}
