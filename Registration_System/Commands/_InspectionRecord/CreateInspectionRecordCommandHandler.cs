using MediatR;
using Registration_System.Models;

namespace Registration_System.Commands._InspectionRecord
{
    public class CreateInspectionRecordCommandHandler : IRequestHandler<CreateInspectionRecordCommand, Guid>
    {
        private readonly RegistrationSystemDbContext _context;

        public CreateInspectionRecordCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateInspectionRecordCommand request, CancellationToken cancellationToken)
        {
            var inspectionRecord = new InspectionRecord
            {
                VehicleId = request.VehicleId,
                InspectionDate = request.InspectionDate,
                Result = request.Result,
                InspectorName = request.InspectorName,
                IsDeleted = request.IsDeleted
            };

            _context.InspectionRecords.Add(inspectionRecord);
            await _context.SaveChangesAsync(cancellationToken);

            return inspectionRecord.InspectionId;
        }
    }

}
