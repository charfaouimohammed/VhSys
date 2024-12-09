using MediatR;

namespace Registration_System.Commands._LicensePlate
{
    public class DeleteLicensePlateCommand : IRequest<bool>
    {
        public Guid LicensePlateId { get; set; }
    }

}
