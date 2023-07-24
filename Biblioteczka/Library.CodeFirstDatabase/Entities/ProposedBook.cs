using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.CodeFirstDatabase.Entities
{
    public class ProposedBook
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public Uri UrlLink { get; set; }
        public int Points { get; set; }
        public string Authors { get; set; }
        public string Categories { get; set; }
        public Uri Cover { get; set; }

        //Relation

        public ICollection<User> Users { get; set; } = new List<User>();


    }
}
