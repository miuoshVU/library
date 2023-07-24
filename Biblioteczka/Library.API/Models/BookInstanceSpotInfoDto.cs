using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.CodeFirstDatabase.Enum;

namespace Library.API.Models
{
    public class BookInstanceSpotInfoDto
    {
        public string Name { get; set; }
        public string Building { get; set; }
        public int Floor { get; set; }
        public Status Status { get; set; }
        public ICollection<BorrowDto> Borrows { get; set; } = new List<BorrowDto>();
        public BookDto Book { get; set; }
        public int BookId { get; set; }
    }
}
