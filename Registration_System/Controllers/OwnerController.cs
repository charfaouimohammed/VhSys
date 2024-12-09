using MediatR;
using Microsoft.AspNetCore.Mvc;
using Registration_System.Commands._Owner;
using Registration_System.DTOs;
using Registration_System.Querys._Owner;

namespace Registration_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OwnersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOwnerById(Guid id)
        {
            var result = await _mediator.Send(new GetOwnerByIdQuery { OwnerId = id });
            return Ok(result);
        }

        // POST: api/Owner
        [HttpPost]
        public async Task<IActionResult> CreateOwner([FromBody] CreateOwnerCommand command)
        {
            if (command == null)
            {
                return BadRequest("Owner data is required.");
            }

            // Call the CreateOwnerCommandHandler via MediatR
            var ownerDto = await _mediator.Send(command);

            // Return a 201 Created response with the created OwnerDto
            // Ensure that the action name matches the GetOwnerById action method
            return CreatedAtAction(nameof(GetOwnerById), new { id = ownerDto.OwnerId }, ownerDto);
        }

        // PUT: api/Owner/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateOwner(Guid id, [FromBody] UpdateOwnerCommand command)
        {
            if (id != command.OwnerId)
            {
                return BadRequest("Owner ID in the route and body do not match.");
            }

            try
            {
                await _mediator.Send(command);
                return NoContent(); // 204 No Content indicates the update was successful
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message }); // Return 404 if owner is not found
            }
        }

        // DELETE: api/Owner/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteOwner(Guid id)
        {
            var command = new DeleteOwnerCommand { OwnerId = id };

            var result = await _mediator.Send(command);

            if (result)
            {
                // Return 204 No Content when deletion is successful
                return NoContent();
            }

            // Return 404 Not Found if the owner does not exist or is already deleted
            return NotFound(new { message = "Owner not found or already deleted." });
        }

        // Get All Owners
        [HttpGet]
            public async Task<IActionResult> GetAllOwners()
            {
                var owners = await _mediator.Send(new GetAllOwnersQuery());
                return Ok(owners); // Return all owners with driver licenses and categories
            }
        
    }

}
