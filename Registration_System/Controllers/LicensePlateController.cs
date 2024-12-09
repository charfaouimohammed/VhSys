using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration_System.Commands._LicensePlate;
using Registration_System.DTOs;
using Registration_System.Querys._LicensePlate;

namespace Registration_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LicensePlateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LicensePlateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateLicensePlate([FromBody] CreateLicensePlateCommand command)
        {
            var licensePlateId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetLicensePlate), new { id = licensePlateId }, licensePlateId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LicensePlateDTO>> GetLicensePlate(Guid id)
        {
            var query = new GetLicensePlateQuery { LicensePlateId = id };
            var licensePlate = await _mediator.Send(query);

            if (licensePlate == null)
            {
                return NotFound();
            }

            return Ok(licensePlate);
        }

        [HttpGet]
        public async Task<ActionResult<List<LicensePlateDTO>>> GetAllLicensePlates()
        {
            var query = new GetAllLicensePlatesQuery();
            var licensePlates = await _mediator.Send(query);
            return Ok(licensePlates);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLicensePlate(Guid id, [FromBody] UpdateLicensePlateCommand command)
        {
            command.LicensePlateId = id;
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLicensePlate(Guid id)
        {
            var command = new DeleteLicensePlateCommand { LicensePlateId = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}
