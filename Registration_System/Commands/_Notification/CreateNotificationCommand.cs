using MediatR;

namespace Registration_System.Commands._Notification
{
    public class CreateNotificationCommand : IRequest<Guid>
    {
        public Guid? OwnerId { get; set; }
        public string? Message { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? Status { get; set; }
        public bool? IsDeleted { get; set; }
    }

}
