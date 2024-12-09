using MediatR;

namespace Registration_System.Commands._InspectionRecord
{
    public class CreateInspectionRecordCommand : IRequest<Guid>
    {
        public Guid? VehicleId { get; set; }
        public DateOnly? InspectionDate { get; set; }
        public string? Result { get; set; }
        public string? InspectorName { get; set; }
        public bool? IsDeleted { get; set; }
    }

}
