using MediatR;
using Registration_System.DTOs;
using System;

namespace Registration_System.Queries._DriverLicense
{
    public class GetDriverLicenseByIdQuery : IRequest<DriverLicenseDTO>
    {
        public Guid LicenseId { get; set; }
        public GetDriverLicenseByIdQuery(Guid licenseId)
        {
            LicenseId = licenseId;
        }
    }
}
