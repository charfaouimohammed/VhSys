using MediatR;
using Microsoft.AspNetCore.Mvc;
using Registration_System.Commands._Administration;
using Registration_System.Querys._Administration;

[ApiController]
[Route("api/[controller]")]
public class AdministrationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdministrationController(IMediator mediator)
    {
        _mediator = mediator;
    }



    [HttpPost]
    public async Task<IActionResult> CreateAdministration([FromBody] CreateAdministrationCommand command)
    {
        var adminId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAdministrationById), new { id = adminId }, adminId);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAdministrations()
    {
        var admins = await _mediator.Send(new GetAllAdministrationsQuery());
        return Ok(admins);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAdministrationById(Guid id)
    {
        var admin = await _mediator.Send(new GetAdministrationByIdQuery { AdminId = id });
        return Ok(admin);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAdministration(Guid id, [FromBody] UpdateAdministrationCommand command)
    {
        command.AdminId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAdministration(Guid id)
    {
        await _mediator.Send(new DeleteAdministrationCommand { AdminId = id });
        return NoContent();
    }
}
