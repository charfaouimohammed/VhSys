using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration_System.Commands._Insurance;
using Registration_System.DTOs;
using Registration_System.Querys._Insurance;

namespace Registration_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsuranceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InsuranceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateInsurance([FromBody] CreateInsuranceCommand command)
        {
            var insuranceId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetInsurance), new { id = insuranceId }, insuranceId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InsuranceDTO>> GetInsurance(Guid id)
        {
            var query = new GetInsuranceQuery { InsuranceId = id };
            var insurance = await _mediator.Send(query);

            if (insurance == null)
            {
                return NotFound();
            }

            return Ok(insurance);
        }

        [HttpGet]
        public async Task<ActionResult<List<InsuranceDTO>>> GetAllInsurances()
        {
            var query = new GetAllInsurancesQuery();
            var insurances = await _mediator.Send(query);
            return Ok(insurances);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateInsurance(Guid id, [FromBody] UpdateInsuranceCommand command)
        {
            command.InsuranceId = id;
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInsurance(Guid id)
        {
            var command = new DeleteInsuranceCommand { InsuranceId = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}
