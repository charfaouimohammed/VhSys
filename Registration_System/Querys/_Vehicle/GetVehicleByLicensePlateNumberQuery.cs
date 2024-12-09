using MediatR;
using Registration_System.DTOs;
using System;

namespace Registration_System.Querys._Vehicle
{
    public class GetVehicleByLicensePlateNumberQuery : IRequest<VehicleDTO>
    {
        public string LicensePlateNumber { get; set; }

        public GetVehicleByLicensePlateNumberQuery(string licensePlateNumber)
        {
            LicensePlateNumber = licensePlateNumber;
        }
    }
}
