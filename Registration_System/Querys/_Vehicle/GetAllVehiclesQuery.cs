using MediatR;
using Registration_System.DTOs;
using System.Collections.Generic;

namespace Registration_System.Querys._Vehicle;

public class GetAllVehiclesQuery : IRequest<List<VehicleDTO>> { }
