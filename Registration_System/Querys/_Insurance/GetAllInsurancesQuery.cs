using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._Insurance
{
    public class GetAllInsurancesQuery : IRequest<List<InsuranceDTO>>
    {
    }


}
