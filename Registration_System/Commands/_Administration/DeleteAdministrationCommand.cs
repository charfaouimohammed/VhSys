using MediatR;

namespace Registration_System.Commands._Administration
{
    public class DeleteAdministrationCommand : IRequest<Unit>
    {
        public Guid AdminId { get; set; }
    }
}
