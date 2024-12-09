using MediatR;
using Registration_System.DTOs;
using Registration_System.Models;

namespace Registration_System.Querys._Vehicle
{
    public class GetOwnershipHistoryByLicensePlateQuery : IRequest<List<OwnershipHistoryDTO>>
    {
        public string LicensePlate { get; set; }

        public GetOwnershipHistoryByLicensePlateQuery(string licensePlate)
        {
            LicensePlate = licensePlate;
        }
    }
}
