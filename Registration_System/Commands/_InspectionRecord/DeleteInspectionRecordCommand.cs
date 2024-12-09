using MediatR;

namespace Registration_System.Commands._InspectionRecord
{
    public class DeleteInspectionRecordCommand : IRequest<bool>
    {
        public Guid InspectionId { get; set; }
    }

}
