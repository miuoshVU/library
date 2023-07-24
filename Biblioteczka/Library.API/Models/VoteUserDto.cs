using Library.CodeFirstDatabase.Entities;

namespace Library.API.Models
{
    public class VoteUserDto
    {
        public Guid UserId { get; set; }
        public ICollection<int> ProposedBooksId { get; set; } = new List<int>();
    }
}
