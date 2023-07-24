using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.CodeFirstDatabase.Enum;

namespace Library.API.Models
{
    public class BookInstanceDto
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public string OwnerName { get; set; }
        public string? QR { get; set; }

        //Relation
        public BookDto Book { get; set; }
        public int BookId { get; set; }
        public SpotDto Spot { get; set; }
        public int SpotId { get; set; }
        public ICollection<BorrowDto> Borrows { get; set; } = new List<BorrowDto>();
       

    }
}
