namespace Registration_System.DTOs
{
    public class InsuranceDTO
    {
        public Guid InsuranceId { get; set; }
        public Guid? VehicleId { get; set; }
        public string? PolicyNumber { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Provider { get; set; }
        public bool? IsDeleted { get; set; }
    }

}
