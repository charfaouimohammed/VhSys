using MediatR;
using Registration_System.Models;

namespace Registration_System.Commands._InspectionRecord
{
    public class DeleteInspectionRecordCommandHandler : IRequestHandler<DeleteInspectionRecordCommand, bool>
    {
        private readonly RegistrationSystemDbContext _context;

        public DeleteInspectionRecordCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteInspectionRecordCommand request, CancellationToken cancellationToken)
        {
            var inspectionRecord = await _context.InspectionRecords.FindAsync(request.InspectionId);
            if (inspectionRecord == null)
            {
                return false;
            }

            _context.InspectionRecords.Remove(inspectionRecord);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
