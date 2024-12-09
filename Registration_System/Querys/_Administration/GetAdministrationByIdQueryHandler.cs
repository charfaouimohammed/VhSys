using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;
using Registration_System.Querys._Administration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Queries._Administration
{
    public class GetAdministrationByIdQueryHandler : IRequestHandler<GetAdministrationByIdQuery, AdministrationDTO>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetAdministrationByIdQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<AdministrationDTO> Handle(GetAdministrationByIdQuery request, CancellationToken cancellationToken)
        {
            var admin = await _context.Administrations
                .Where(a => a.AdminId == request.AdminId && a.IsDeleted == false)
                .Select(a => new AdministrationDTO
                {
                    AdminId = a.AdminId,
                    Username = a.Username,
                    Fullname = a.Fullname,
                    Email = a.Email,
                    Role = a.Role,
                    IsActif = a.IsActif ?? false
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (admin == null)
                throw new Exception("Administration not found.");

            return admin;
        }
    }
}
