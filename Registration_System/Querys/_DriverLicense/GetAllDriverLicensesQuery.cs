using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._DriverLicense
{
    public class GetAllDriverLicensesQuery : IRequest<List<DriverLicenseDTO>>
    {
    }

}
