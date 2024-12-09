using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._TrafficViolation
{
    public class GetAllTrafficViolationsQuery : IRequest<List<TrafficViolationDTO>>
    {
    }

}
