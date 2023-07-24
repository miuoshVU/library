using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.CodeFirstDatabase.Entities
{
    public class Password
    {
        [Key]
        public Guid Id { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
        public int Rounds { get; set; }

        //Relation
        public User User { get; set; }

       
    }
}
