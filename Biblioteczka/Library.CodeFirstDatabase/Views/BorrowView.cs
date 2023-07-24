using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.CodeFirstDatabase.Views
{
    public class BorrowView
    {
          public Guid Id { get; set; }
          public int BookInstanceId { get; set; } 
          public Guid UserId { get; set; }
    }
}
