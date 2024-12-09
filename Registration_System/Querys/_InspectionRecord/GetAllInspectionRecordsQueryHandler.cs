using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;

namespace Registration_System.Querys._InspectionRecord
{
    public class GetAllInspectionRecordsQueryHandler : IRequestHandler<GetAllInspectionRecordsQuery, List<InspectionRecordDTO>>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetAllInspectionRecordsQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<InspectionRecordDTO>> Handle(GetAllInspectionRecordsQuery request, CancellationToken cancellationToken)
        {
            var inspectionRecords = await _context.InspectionRecords
                .Select(ir => new InspectionRecordDTO
                {
                    InspectionId = ir.InspectionId,
                    VehicleId = ir.VehicleId,
                    InspectionDate = ir.InspectionDate,
                    Result = ir.Result,
                    InspectorName = ir.InspectorName,
                    IsDeleted = ir.IsDeleted
                })
                .ToListAsync(cancellationToken);

            return inspectionRecords;
        }
    }

}
