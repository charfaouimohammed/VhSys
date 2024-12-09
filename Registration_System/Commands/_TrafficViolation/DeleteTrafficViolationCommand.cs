using MediatR;

namespace Registration_System.Commands._TrafficViolation
{
    public class DeleteTrafficViolationCommand : IRequest<bool>
    {
        public Guid ViolationId { get; set; }
    }

}
