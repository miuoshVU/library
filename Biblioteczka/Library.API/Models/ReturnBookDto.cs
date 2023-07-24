using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.CodeFirstDatabase.Enum;

namespace Library.API.Models
{
    public class ReturnBookDto
    {
        public int Id { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public Status? Status { get; set; }

        //Relation
        public int BookInstanceId { get; set; }
        public Guid UserId { get; set; }
        public ICollection<BookInstanceDto> bookInstanceDtos { get; set; }
    }
}
