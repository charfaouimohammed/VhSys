using MediatR;
using System;

namespace Registration_System.Commands._DriverLicense
{
    public class DeleteDriverLicenseCommand : IRequest<Unit>
    {
        public Guid LicenseId { get; set; }

        public DeleteDriverLicenseCommand(Guid licenseId)
        {
            LicenseId = licenseId;
        }
    }
}
