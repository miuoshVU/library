using Library.CodeFirstDatabase.Entities;

namespace Library.API.Models
{
    public class PasswordDto
    {
        public Guid Id { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
        public int Rounds { get; set; }

        public virtual User User { get; set; }
        public Guid UserId { get; set; }
    }
}