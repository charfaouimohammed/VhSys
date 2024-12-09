namespace Registration_System.DTOs
{
    public class OwnershipHistoryDTO
    {
        public Guid OwnershipId { get; set; }
        public Guid? VehicleId { get; set; }
        public Guid? OwnerId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? OwnerName { get; set; }
        public bool? IsDeleted { get; set; }
        public bool IsCurrentOwner { get; set; }

    }

}
