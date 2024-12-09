using MediatR;
using Registration_System.DTOs;
using System;

namespace Registration_System.Querys._Vehicle;

public class GetVehicleByIdQuery : IRequest<VehicleDTO>
{
    public Guid VehicleId { get; set; }

    public GetVehicleByIdQuery(Guid vehicleId)
    {
        VehicleId = vehicleId;
    }
}
