using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._Insurance
{
    public class GetInsuranceQuery : IRequest<InsuranceDTO>
    {
        public Guid InsuranceId { get; set; }
    }

}
