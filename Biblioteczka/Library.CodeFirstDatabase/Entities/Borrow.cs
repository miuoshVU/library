using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.CodeFirstDatabase.Enum;

namespace Library.CodeFirstDatabase.Entities
{
    public class Borrow
    {
        [Key]
        public int Id { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public Status? Status { get; set; }

        //Relation
        public BookInstance BookInstance { get; set; }
        public int BookInstanceId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }

    }
}
