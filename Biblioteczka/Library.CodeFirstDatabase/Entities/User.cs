using Library.CodeFirstDatabase.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Library.CodeFirstDatabase.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public Uri? Avatar { get; set; }
        public int RemainingVotes { get; set; }
        public Notification Notifications { get; set; }

        //Relation
        public ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();
        public Password Password { get; set; }
        public Guid PasswordId { get; set; }
        public ICollection<ProposedBook> ProposedBooks{get; set;} = new List<ProposedBook>();
        public ICollection<LogEntry> LogEntries { get; set; } = new List<LogEntry>();

    }
}
