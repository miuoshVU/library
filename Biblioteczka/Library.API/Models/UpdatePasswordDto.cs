namespace Library.API.Models
{
    public class UpdatePasswordDto
    {
        public Guid UserId { get; set; }
        public string Paswd { get; set; }
    }
}
