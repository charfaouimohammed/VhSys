using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._TrafficViolation
{
    public class GetTrafficViolationQuery : IRequest<TrafficViolationDTO>
    {
        public Guid ViolationId { get; set; }
    }

}
