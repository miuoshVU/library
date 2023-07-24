using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.CodeFirstDatabase.Entities
{
    public class Category
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public Uri? Cover { get; set; }

        //Relation
        public ICollection<Book> Books { get; set; } = new List<Book>();
        public ICollection<ProposedBook> ProposedBooks { get; set; } = new List<ProposedBook>();

        //Override ToString()
        public override string ToString()
        {
            return $"{Name}";
        }

    }
}
