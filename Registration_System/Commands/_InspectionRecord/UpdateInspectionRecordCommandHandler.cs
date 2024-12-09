using MediatR;
using Registration_System.Models;

namespace Registration_System.Commands._InspectionRecord
{
    public class UpdateInspectionRecordCommandHandler : IRequestHandler<UpdateInspectionRecordCommand, bool>
    {
        private readonly RegistrationSystemDbContext _context;

        public UpdateInspectionRecordCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateInspectionRecordCommand request, CancellationToken cancellationToken)
        {
            var inspectionRecord = await _context.InspectionRecords.FindAsync(request.InspectionId);
            if (inspectionRecord == null)
            {
                return false;
            }

            inspectionRecord.VehicleId = request.VehicleId;
            inspectionRecord.InspectionDate = request.InspectionDate;
            inspectionRecord.Result = request.Result;
            inspectionRecord.InspectorName = request.InspectorName;
            inspectionRecord.IsDeleted = request.IsDeleted;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
