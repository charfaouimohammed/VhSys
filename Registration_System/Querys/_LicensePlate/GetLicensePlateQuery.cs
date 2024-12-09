using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._LicensePlate
{
    public class GetLicensePlateQuery : IRequest<LicensePlateDTO>
    {
        public Guid LicensePlateId { get; set; }
    }

}
