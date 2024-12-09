namespace Registration_System.DTOs
{
    public class InspectionRecordDTO
    {
        public Guid InspectionId { get; set; }
        public Guid? VehicleId { get; set; }
        public DateOnly? InspectionDate { get; set; }
        public string? Result { get; set; }
        public string? InspectorName { get; set; }
        public bool? IsDeleted { get; set; }
    }

}
