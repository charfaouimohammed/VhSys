using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration_System.Commands._Vehicle;
using Registration_System.DTOs;
using Registration_System.Querys._Vehicle;

namespace Registration_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehicleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<VehicleDTO>>> GetAllVehicles()
        {
            var vehicles = await _mediator.Send(new GetAllVehiclesQuery());
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDTO>> GetVehicleById(Guid id)
        {
            var vehicle = await _mediator.Send(new GetVehicleByIdQuery(id));
            return Ok(vehicle);
        }
        /// <summary>
        /// Creates a new vehicle and automatically generates a license plate.
        /// </summary>
        /// <param name="command">The vehicle creation command.</param>
        /// <returns>The ID of the newly created vehicle.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var vehicleId = await _mediator.Send(command);
                return Ok(new { VehicleId = vehicleId, Message = "Vehicle created successfully with license plate generated." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        [HttpPut("{vehicleId}")]
        public async Task<IActionResult> UpdateVehicle(Guid vehicleId, [FromBody] UpdateVehicleCommand command)
        {
            if (vehicleId != command.VehicleId)
                return BadRequest("Vehicle ID mismatch.");

            var result = await _mediator.Send(command);
            return result ? Ok("Vehicle updated successfully.") : StatusCode(500, "Failed to update vehicle.");
        }

        [HttpDelete("{vehicleId}")]
        public async Task<IActionResult> DeleteVehicle(Guid vehicleId)
        {
            var command = new DeleteVehicleCommand { VehicleId = vehicleId };
            var result = await _mediator.Send(command);
            return result ? Ok("Vehicle deleted successfully.") : StatusCode(500, "Failed to delete vehicle.");
        }

        /// <summary>
        /// Gets a vehicle by its license plate number.
        /// </summary>
        /// <param name="licensePlateNumber">The license plate number of the vehicle.</param>
        /// <returns>The vehicle details if found, or NotFound if not found.</returns>
        [HttpGet("GetByLicensePlate/{licensePlateNumber}")]
        public async Task<IActionResult> GetByLicensePlate(string licensePlateNumber)
        {
            var query = new GetVehicleByLicensePlateNumberQuery(licensePlateNumber);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound(new { message = "Vehicle not found" });

            return Ok(result);
        }

        [HttpPost("transfer-ownership")]
        public async Task<IActionResult> TransferOwnership([FromBody] TransferVehicleOwnershipCommand command)
        {
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok(new { message = "Ownership transfer successful." });
            }
            else
            {
                return BadRequest(new { message = "Ownership transfer failed. The new owner may already own this vehicle." });
            }
        }

        [HttpGet("owners-by-license-plate/{licensePlate}")]
        public async Task<IActionResult> GetOwnershipHistoryByLicensePlate(string licensePlate)
        {
            var query = new GetOwnershipHistoryByLicensePlateQuery(licensePlate);
            var result = await _mediator.Send(query);

            if (result.Count == 0)
            {
                return NotFound(new { message = "No ownership history found for the provided license plate." });
            }

            return Ok(result);
        }

    }

}
