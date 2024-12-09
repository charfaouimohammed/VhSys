using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.DTOs;
using Registration_System.Models;

namespace Registration_System.Querys._InspectionRecord
{
    public class GetInspectionRecordQueryHandler : IRequestHandler<GetInspectionRecordQuery, InspectionRecordDTO>
    {
        private readonly RegistrationSystemDbContext _context;

        public GetInspectionRecordQueryHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<InspectionRecordDTO> Handle(GetInspectionRecordQuery request, CancellationToken cancellationToken)
        {
            var inspectionRecord = await _context.InspectionRecords
                .Where(ir => ir.InspectionId == request.InspectionId)
                .FirstOrDefaultAsync(cancellationToken);

            if (inspectionRecord == null)
            {
                return null;
            }

            return new InspectionRecordDTO
            {
                InspectionId = inspectionRecord.InspectionId,
                VehicleId = inspectionRecord.VehicleId,
                InspectionDate = inspectionRecord.InspectionDate,
                Result = inspectionRecord.Result,
                InspectorName = inspectionRecord.InspectorName,
                IsDeleted = inspectionRecord.IsDeleted
            };
        }
    }

}
