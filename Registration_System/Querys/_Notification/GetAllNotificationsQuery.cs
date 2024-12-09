using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._Notification
{
    public class GetAllNotificationsQuery : IRequest<List<NotificationDTO>>
    {
    }

}
