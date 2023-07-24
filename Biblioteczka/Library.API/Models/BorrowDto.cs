using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.CodeFirstDatabase.Enum;

namespace Library.API.Models
{
    public class BorrowDto
    {
        public int Id { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public Status?  Status { get; set; }
        public List<BookInstanceDto> bookInstances { get; set; }
        
    }
}
