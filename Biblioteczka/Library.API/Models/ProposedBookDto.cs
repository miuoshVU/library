using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class ProposedBookDto
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public Uri UrlLink { get; set; }
        public int Points { get; set; }
        public Uri Cover { get; set; } = new Uri("https://lexliber.pl/userdata/public/gfx/15444/default.jpg");
        public string Authors { get; set; }
        public string Categories { get; set; }
    }
}
