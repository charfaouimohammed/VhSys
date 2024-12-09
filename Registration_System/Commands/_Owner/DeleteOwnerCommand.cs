using MediatR;
using System;

namespace Registration_System.Commands._Owner
{
   public class DeleteOwnerCommand : IRequest<bool>
   {
    public Guid OwnerId { get; set; }
   }
}
