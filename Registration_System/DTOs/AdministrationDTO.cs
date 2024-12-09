namespace Registration_System.DTOs
{
    public class AdministrationDTO
    {
        public Guid AdminId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsActif { get; set; }
    }

}
