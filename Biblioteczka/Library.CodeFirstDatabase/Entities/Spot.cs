using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.CodeFirstDatabase.Entities
{
    public class Spot
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Building { get; set; }
        public int Floor { get; set; }
        public string? Description { get; set; }
        public string? QR { get; set; }

        //Relation
        public ICollection<BookInstance> BookInstances { get; set; } = new List<BookInstance>();
        //Override ToString
        public override string ToString()
        {
            return $"{Name} {Building} {Description}";
        }

    }
}
