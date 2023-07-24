using Library.CodeFirstDatabase.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.CodeFirstDatabase.Entities
{
    public class BookInstance 
    {
        [Key]
        public int Id { get; set; }
        public Status Status { get; set; }
        public string OwnerName { get; set; }
        public string? QR { get; set; }          

        //Relation
        public Book Book { get; set; }
        public int BookId { get; set; }
        public Spot Spot { get; set; }
        public int SpotId { get; set; }
        public ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();

        //Override ToString
        public override string ToString()
        {
            return $"{Status} {QR}";
        }


    }
}
