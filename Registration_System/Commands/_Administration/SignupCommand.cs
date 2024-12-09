namespace Registration_System.Commands._Administration
{
    using MediatR;

    namespace Registration_System.Commands._Administration
    {
        public class SignupCommand : IRequest<Guid>
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string Fullname { get; set; }
            public string Password { get; set; }
            public string? Role { get; set; }
            public bool IsActif { get; set; }
        }
    }

}
