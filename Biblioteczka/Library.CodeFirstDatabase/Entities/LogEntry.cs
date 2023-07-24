using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.CodeFirstDatabase.Enum;

namespace Library.CodeFirstDatabase.Entities
{
    public class LogEntry
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public OperationType Operation { get; set; }

        //Relation
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
