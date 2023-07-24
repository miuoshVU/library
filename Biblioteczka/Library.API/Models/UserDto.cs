using Library.CodeFirstDatabase.Entities;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Role { get; set; }
        public Uri? Avatar { get; set; }
        public int RemainingVotes { get; set; }

        public List<Borrow> Borrows { get; set; } = new List<Borrow>();
        public ICollection<ProposedBook> ProposedBooks { get; set; } = new List<ProposedBook>();
    }
}