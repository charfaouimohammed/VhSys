
namespace Registration_System.DTOs
{

    public class VehicleDTO
    {
        public Guid VehicleId { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }
        public string? Color { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CurrentOwner { get; set; }
        public string? RegistrationOffice { get; set; }
        public string? LicensePlateNumber { get; set; }
        public IEnumerable<OwnershipHistoryDTO> OwnershipHistory { get; set; } = new List<OwnershipHistoryDTO>();
    }

}
