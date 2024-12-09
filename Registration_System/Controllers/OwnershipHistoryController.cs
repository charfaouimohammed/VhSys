using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration_System.Commands._OwnershipHistory;
using Registration_System.DTOs;
using Registration_System.Querys._OwnershipHistory;

namespace Registration_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnershipHistoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OwnershipHistoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateOwnershipHistory([FromBody] CreateOwnershipHistoryCommand command)
        {
            var ownershipHistoryId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetOwnershipHistory), new { id = ownershipHistoryId }, ownershipHistoryId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OwnershipHistoryDTO>> GetOwnershipHistory(Guid id)
        {
            var query = new GetOwnershipHistoryQuery { OwnershipId = id };
            var ownershipHistory = await _mediator.Send(query);

            if (ownershipHistory == null)
            {
                return NotFound();
            }

            return Ok(ownershipHistory);
        }

        [HttpGet]
        public async Task<ActionResult<List<OwnershipHistoryDTO>>> GetAllOwnershipHistories()
        {
            var query = new GetAllOwnershipHistoriesQuery();
            var ownershipHistories = await _mediator.Send(query);
            return Ok(ownershipHistories);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOwnershipHistory(Guid id, [FromBody] UpdateOwnershipHistoryCommand command)
        {
            command.OwnershipId = id;
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOwnershipHistory(Guid id)
        {
            var command = new DeleteOwnershipHistoryCommand { OwnershipId = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}
