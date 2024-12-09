using MediatR;
using Registration_System.Models;

namespace Registration_System.Commands._RegistrationOffice
{
    public class CreateRegistrationOfficeCommandHandler : IRequestHandler<CreateRegistrationOfficeCommand, Guid>
    {
        private readonly RegistrationSystemDbContext _context;

        public CreateRegistrationOfficeCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateRegistrationOfficeCommand request, CancellationToken cancellationToken)
        {
            var registrationOffice = new RegistrationOffice
            {
                OfficeName = request.OfficeName,
                Address = request.Address,
                AdminId = request.AdminId
            };

            _context.RegistrationOffices.Add(registrationOffice);
            await _context.SaveChangesAsync(cancellationToken);

            return registrationOffice.OfficeId;
        }
    }

}
