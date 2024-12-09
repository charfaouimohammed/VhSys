using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;
using Registration_System.Querys._Administration;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Queries._Administration
{
    public class GetAllAdministrationsQueryHandler : IRequestHandler<GetAllAdministrationsQuery, List<AdministrationDTO>>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetAllAdministrationsQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<AdministrationDTO>> Handle(GetAllAdministrationsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Administrations
                .Where(admin => admin.IsDeleted == false)
                .Select(admin => new AdministrationDTO
                {
                    AdminId = admin.AdminId,
                    Username = admin.Username,
                    Fullname = admin.Fullname,
                    Email = admin.Email,
                    Role = admin.Role,
                    IsActif = admin.IsActif ?? false
                })
                .ToListAsync(cancellationToken);
        }
    }
}
