﻿using MediatR;

namespace Registration_System.Commands._OwnershipHistory
{
    public class CreateOwnershipHistoryCommand : IRequest<Guid>
    {
        public Guid? VehicleId { get; set; }
        public Guid? OwnerId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool IsCurrentOwner { get; set; }
    }

}
