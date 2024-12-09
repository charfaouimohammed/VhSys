using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration_System.Commands._RegistrationOffice;
using Registration_System.DTOs;
using Registration_System.Querys._RegistrationOffice;

namespace Registration_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationOfficeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegistrationOfficeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateRegistrationOffice([FromBody] CreateRegistrationOfficeCommand command)
        {
            var registrationOfficeId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetRegistrationOffice), new { id = registrationOfficeId }, registrationOfficeId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegistrationOfficeDTO>> GetRegistrationOffice(Guid id)
        {
            var query = new GetRegistrationOfficeQuery { OfficeId = id };
            var registrationOffice = await _mediator.Send(query);

            if (registrationOffice == null)
            {
                return NotFound();
            }

            return Ok(registrationOffice);
        }

        [HttpGet]
        public async Task<ActionResult<List<RegistrationOfficeDTO>>> GetAllRegistrationOffices()
        {
            var query = new GetAllRegistrationOfficesQuery();
            var registrationOffices = await _mediator.Send(query);
            return Ok(registrationOffices);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRegistrationOffice(Guid id, [FromBody] UpdateRegistrationOfficeCommand command)
        {
            command.OfficeId = id;
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRegistrationOffice(Guid id)
        {
            var command = new DeleteRegistrationOfficeCommand { OfficeId = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}
