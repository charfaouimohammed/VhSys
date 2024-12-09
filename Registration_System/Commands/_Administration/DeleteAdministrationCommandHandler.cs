using MediatR;
using Registration_System.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Commands._Administration
{
    public class DeleteAdministrationCommandHandler : IRequestHandler<DeleteAdministrationCommand, Unit>
    {
        private readonly RegistrationSystemDbContext _context;

        public DeleteAdministrationCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteAdministrationCommand request, CancellationToken cancellationToken)
        {
            var admin = await _context.Administrations.FindAsync(request.AdminId);
            if (admin == null || admin.IsDeleted == true)
                throw new Exception("Administration not found or is already deleted.");

            admin.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
