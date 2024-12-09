using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._LicensePlate
{
    public class GetAllLicensePlatesQuery : IRequest<List<LicensePlateDTO>>
    {
    }

}
