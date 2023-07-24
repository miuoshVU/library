using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.CodeFirstDatabase.Entities
{
    public class Book
    
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ISBN { get; set; }
        [MaxLength(4)]
        public int? YearOfPublication { get; set; }
        public string? Description { get; set; }
        public string?  Publisher { get; set; }
        public Uri? Cover { get; set; }
        public int? Pages { get; set; }

        //Relation
        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<BookInstance> BookInstances { get; set; } = new List<BookInstance>();
        //public ProposedBook ProposedBook { get; set; }

        //override ToString()
        public override string ToString()
        {
            return $"{Title} {Publisher}";
        }
    }
}
