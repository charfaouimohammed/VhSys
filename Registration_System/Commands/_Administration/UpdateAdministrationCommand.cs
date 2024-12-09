using MediatR;

namespace Registration_System.Commands._Administration
{
    public class UpdateAdministrationCommand : IRequest<Unit>
    {
        public Guid AdminId { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
        public string? Fullname { get; set; }
        public bool? IsActif { get; set; }
    }
}
