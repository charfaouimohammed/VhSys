using MediatR;

namespace Registration_System.Commands._InspectionRecord
{
    public class UpdateInspectionRecordCommand : IRequest<bool>
    {
        public Guid InspectionId { get; set; }
        public Guid? VehicleId { get; set; }
        public DateOnly? InspectionDate { get; set; }
        public string? Result { get; set; }
        public string? InspectorName { get; set; }
        public bool? IsDeleted { get; set; }
    }

}
