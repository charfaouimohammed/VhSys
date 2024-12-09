using MediatR;
using Registration_System.DTOs;
using System.ComponentModel;

namespace Registration_System.Querys._Administration
{
    public class GetAdministrationByIdQuery : IRequest<AdministrationDTO>
    {
        public Guid AdminId { get; set; }

    }
    

}
