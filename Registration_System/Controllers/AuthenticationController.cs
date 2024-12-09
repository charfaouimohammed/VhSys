using MediatR;
using Microsoft.AspNetCore.Mvc;
using Registration_System.Commands._Administration;
using Registration_System.Commands._Administration.Registration_System.Commands._Administration;
using System;
using System.Threading.Tasks;

namespace Registration_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Login endpoint to authenticate user and return JWT token
        /// </summary>
        /// <param name="command">LoginCommand with username and password</param>
        /// <returns>JWT token on successful login</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            if (command == null)
            {
                return BadRequest("Invalid login details.");
            }

            try
            {
                // Send the LoginCommand to the handler to authenticate and generate token
                var token = await _mediator.Send(command);

                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid credentials.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Signup endpoint to register a new user (Admin/Employee)
        /// </summary>
        /// <param name="command">SignupCommand with user details</param>
        /// <returns>User GUID after successful registration</returns>
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupCommand command)
        {
            if (command == null)
            {
                return BadRequest("Invalid signup details.");
            }

            try
            {
                // Send the SignupCommand to the handler to register the new user
                var userId = await _mediator.Send(command);

                return Ok(new { UserId = userId });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Handle existing username or email
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
