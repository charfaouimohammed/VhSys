using MediatR;

namespace Registration_System.Commands._Insurance
{
    public class CreateInsuranceCommand : IRequest<Guid>
    {
        public Guid? VehicleId { get; set; }
        public string? PolicyNumber { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Provider { get; set; }
        public bool? IsDeleted { get; set; }
    }

}
