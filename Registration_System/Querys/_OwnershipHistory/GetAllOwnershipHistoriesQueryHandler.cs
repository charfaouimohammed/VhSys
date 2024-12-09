using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;

namespace Registration_System.Querys._OwnershipHistory
{
    public class GetAllOwnershipHistoriesQueryHandler : IRequestHandler<GetAllOwnershipHistoriesQuery, List<OwnershipHistoryDTO>>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetAllOwnershipHistoriesQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<OwnershipHistoryDTO>> Handle(GetAllOwnershipHistoriesQuery request, CancellationToken cancellationToken)
        {
            var ownershipHistories = await _context.OwnershipHistories
                .Select(o => new OwnershipHistoryDTO
                {
                    OwnershipId = o.OwnershipId,
                    //VehicleId = o.VehicleId,
                    //OwnerId = o.OwnerId,
                    StartDate = o.StartDate,
                    EndDate = o.EndDate,
                    IsDeleted = o.IsDeleted,
                    IsCurrentOwner = o.IsCurrentOwner
                })
                .ToListAsync(cancellationToken);

            return ownershipHistories;
        }
    }

}
