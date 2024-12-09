using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;

namespace Registration_System.Querys._OwnershipHistory
{
    public class GetOwnershipHistoryQueryHandler : IRequestHandler<GetOwnershipHistoryQuery, OwnershipHistoryDTO>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetOwnershipHistoryQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<OwnershipHistoryDTO> Handle(GetOwnershipHistoryQuery request, CancellationToken cancellationToken)
        {
            var ownershipHistory = await _context.OwnershipHistories
                .Where(o => o.OwnershipId == request.OwnershipId)
                .FirstOrDefaultAsync(cancellationToken);

            if (ownershipHistory == null)
            {
                return null;
            }

            return new OwnershipHistoryDTO
            {
                OwnershipId = ownershipHistory.OwnershipId,
                //VehicleId = ownershipHistory.VehicleId,
                //OwnerId = ownershipHistory.OwnerId,
                StartDate = ownershipHistory.StartDate,
                EndDate = ownershipHistory.EndDate,
                IsDeleted = ownershipHistory.IsDeleted,
                IsCurrentOwner = ownershipHistory.IsCurrentOwner
            };
        }
    }

}
