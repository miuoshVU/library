using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.CodeFirstDatabase.Entities;
using Library.CodeFirstDatabase.Enum;

namespace Library.API.Models
{
    public class AddNewBorrow
    {
        public int Id { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public Status? Status { get; set; }

        //Relation
        public BookInstance bookInstance { get; set; }
        public User user { get; set; }

    }
}
