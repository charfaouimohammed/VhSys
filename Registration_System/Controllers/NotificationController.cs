using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration_System.Commands._Notification;
using Registration_System.DTOs;
using Registration_System.Querys._Notification;

namespace Registration_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateNotification([FromBody] CreateNotificationCommand command)
        {
            var notificationId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetNotification), new { id = notificationId }, notificationId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationDTO>> GetNotification(Guid id)
        {
            var query = new GetNotificationQuery { NotificationId = id };
            var notification = await _mediator.Send(query);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        [HttpGet]
        public async Task<ActionResult<List<NotificationDTO>>> GetAllNotifications()
        {
            var query = new GetAllNotificationsQuery();
            var notifications = await _mediator.Send(query);
            return Ok(notifications);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNotification(Guid id, [FromBody] UpdateNotificationCommand command)
        {
            command.NotificationId = id;
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNotification(Guid id)
        {
            var command = new DeleteNotificationCommand { NotificationId = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}
