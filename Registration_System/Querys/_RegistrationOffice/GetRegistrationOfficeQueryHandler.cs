using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;

namespace Registration_System.Querys._RegistrationOffice
{
    public class GetRegistrationOfficeQueryHandler : IRequestHandler<GetRegistrationOfficeQuery, RegistrationOfficeDTO>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetRegistrationOfficeQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<RegistrationOfficeDTO> Handle(GetRegistrationOfficeQuery request, CancellationToken cancellationToken)
        {
            var registrationOffice = await _context.RegistrationOffices
                .Where(o => o.OfficeId == request.OfficeId)
                .FirstOrDefaultAsync(cancellationToken);

            if (registrationOffice == null)
            {
                return null;
            }

            return new RegistrationOfficeDTO
            {
                OfficeId = registrationOffice.OfficeId,
                OfficeName = registrationOffice.OfficeName,
                Address = registrationOffice.Address,
                AdminId = registrationOffice.AdminId,
                IsDeleted = registrationOffice.IsDeleted
            };
        }
    }

}
