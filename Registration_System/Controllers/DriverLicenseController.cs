using MediatR;
using Microsoft.AspNetCore.Mvc;
using Registration_System.Commands._DriverLicense;
using Registration_System.Queries._DriverLicense;
using Registration_System.DTOs;
using System;
using System.Threading.Tasks;
using Registration_System.Querys._DriverLicense;

namespace Registration_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverLicenseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DriverLicenseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/DriverLicense
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDriverLicenseCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetById), new { id = result }, new { LicenseId = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // GET: api/DriverLicense
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _mediator.Send(new GetAllDriverLicensesQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // GET: api/DriverLicense/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetDriverLicenseByIdQuery(id));
                if (result == null)
                {
                    return NotFound(new { Message = "Driver License not found" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // PUT: api/DriverLicense/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDriverLicenseCommand command)
        {
            if (id != command.LicenseId)
            {
                return BadRequest(new { Message = "License ID mismatch" });
            }

            try
            {
                await _mediator.Send(command);
                return NoContent(); // Indicates the update was successful
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/DriverLicense/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeleteDriverLicenseCommand(id);
                await _mediator.Send(command);
                return NoContent(); // No content is returned for successful deletion
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
