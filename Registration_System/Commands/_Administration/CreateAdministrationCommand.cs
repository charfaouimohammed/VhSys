using MediatR;

namespace Registration_System.Commands._Administration
{
    public class CreateAdministrationCommand : IRequest<Guid>
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
    }
}
