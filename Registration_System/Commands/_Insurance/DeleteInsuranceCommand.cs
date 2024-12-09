using MediatR;

namespace Registration_System.Commands._Insurance
{
    public class DeleteInsuranceCommand : IRequest<bool>
    {
        public Guid InsuranceId { get; set; }
    }

}
