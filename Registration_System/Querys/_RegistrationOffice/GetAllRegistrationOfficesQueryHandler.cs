using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;

namespace Registration_System.Querys._RegistrationOffice
{
    public class GetAllRegistrationOfficesQueryHandler : IRequestHandler<GetAllRegistrationOfficesQuery, List<RegistrationOfficeDTO>>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetAllRegistrationOfficesQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<RegistrationOfficeDTO>> Handle(GetAllRegistrationOfficesQuery request, CancellationToken cancellationToken)
        {
            var registrationOffices = await _context.RegistrationOffices
                .Select(o => new RegistrationOfficeDTO
                {
                    OfficeId = o.OfficeId,
                    OfficeName = o.OfficeName,
                    Address = o.Address,
                    AdminId = o.AdminId,
                    IsDeleted = o.IsDeleted
                })
                .ToListAsync(cancellationToken);

            return registrationOffices;
        }
    }

}
