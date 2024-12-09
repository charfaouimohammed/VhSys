using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration_System.Commands._InspectionRecord;
using Registration_System.DTOs;
using Registration_System.Querys._InspectionRecord;

namespace Registration_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InspectionRecordController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InspectionRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateInspectionRecord([FromBody] CreateInspectionRecordCommand command)
        {
            var inspectionId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetInspectionRecord), new { id = inspectionId }, inspectionId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InspectionRecordDTO>> GetInspectionRecord(Guid id)
        {
            var query = new GetInspectionRecordQuery { InspectionId = id };
            var inspectionRecord = await _mediator.Send(query);

            if (inspectionRecord == null)
            {
                return NotFound();
            }

            return Ok(inspectionRecord);
        }

        [HttpGet]
        public async Task<ActionResult<List<InspectionRecordDTO>>> GetAllInspectionRecords()
        {
            var query = new GetAllInspectionRecordsQuery();
            var inspectionRecords = await _mediator.Send(query);
            return Ok(inspectionRecords);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateInspectionRecord(Guid id, [FromBody] UpdateInspectionRecordCommand command)
        {
            command.InspectionId = id;
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInspectionRecord(Guid id)
        {
            var command = new DeleteInspectionRecordCommand { InspectionId = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}
