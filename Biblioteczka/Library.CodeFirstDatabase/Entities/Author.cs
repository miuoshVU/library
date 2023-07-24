using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.CodeFirstDatabase.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        //Relation
        public ICollection<Book> Books { get; set; } = new List<Book>();
        public ICollection<ProposedBook> ProposedBooks { get; set; } = new List<ProposedBook>();

        //override ToString()
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
