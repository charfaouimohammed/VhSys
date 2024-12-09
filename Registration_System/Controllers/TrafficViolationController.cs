using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration_System.Commands._TrafficViolation;
using Registration_System.DTOs;
using Registration_System.Querys._TrafficViolation;

namespace Registration_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrafficViolationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TrafficViolationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateTrafficViolation([FromBody] CreateTrafficViolationCommand command)
        {
            var violationId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTrafficViolation), new { id = violationId }, violationId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrafficViolationDTO>> GetTrafficViolation(Guid id)
        {
            var query = new GetTrafficViolationQuery { ViolationId = id };
            var violation = await _mediator.Send(query);

            if (violation == null)
            {
                return NotFound();
            }

            return Ok(violation);
        }

        [HttpGet]
        public async Task<ActionResult<List<TrafficViolationDTO>>> GetAllTrafficViolations()
        {
            var query = new GetAllTrafficViolationsQuery();
            var violations = await _mediator.Send(query);
            return Ok(violations);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTrafficViolation(Guid id, [FromBody] UpdateTrafficViolationCommand command)
        {
            command.ViolationId = id;
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrafficViolation(Guid id)
        {
            var command = new DeleteTrafficViolationCommand { ViolationId = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}
