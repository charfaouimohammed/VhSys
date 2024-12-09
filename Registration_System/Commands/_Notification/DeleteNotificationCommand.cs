using MediatR;

namespace Registration_System.Commands._Notification
{
    public class DeleteNotificationCommand : IRequest<bool>
    {
        public Guid NotificationId { get; set; }
    }

}
