using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration_System.Commands._AuditLog;
using Registration_System.DTOs;
using Registration_System.Querys._AuditLog;

namespace Registration_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditLogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuditLogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAuditLog([FromBody] CreateAuditLogCommand command)
        {
            var logId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAuditLog), new { id = logId }, logId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuditLogDTO>> GetAuditLog(Guid id)
        {
            var query = new GetAuditLogQuery { LogId = id };
            var auditLog = await _mediator.Send(query);

            if (auditLog == null)
            {
                return NotFound();
            }

            return Ok(auditLog);
        }

        [HttpGet]
        public async Task<ActionResult<List<AuditLogDTO>>> GetAllAuditLogs()
        {
            var query = new GetAllAuditLogsQuery();
            var auditLogs = await _mediator.Send(query);
            return Ok(auditLogs);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuditLog(Guid id, [FromBody] UpdateAuditLogCommand command)
        {
            command.LogId = id;
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuditLog(Guid id)
        {
            var command = new DeleteAuditLogCommand { LogId = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}
