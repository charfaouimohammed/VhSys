using MediatR;
using Registration_System.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Commands._Administration
{
    public class CreateAdministrationCommandHandler : IRequestHandler<CreateAdministrationCommand, Guid>
    {
        private readonly RegistrationSystemDbContext _context;

        public CreateAdministrationCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateAdministrationCommand request, CancellationToken cancellationToken)
        {
            var admin = new Administration
            {
                AdminId = Guid.NewGuid(),
                Username = request.Username,
                PasswordHash = request.PasswordHash,
                Role = request.Role,
                Email = request.Email,
                Fullname = request.Fullname,
                IsDeleted = false,
                IsActif = true
            };

            _context.Administrations.Add(admin);
            await _context.SaveChangesAsync(cancellationToken);

            return admin.AdminId;
        }
    }
}
