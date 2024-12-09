using MediatR;

namespace Registration_System.Commands._Administration
{
    public class LoginCommand : IRequest<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
