using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._Owner
{
    public class GetOwnerByIdQuery : IRequest<OwnerDto>
    {
        public Guid OwnerId { get; set; }
    }
}
