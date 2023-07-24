using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class AddNewBookDto
    {
       
        public string Title { get; set; }
        [MaxLength(13)]
        public string? ISBN { get; set; }
        public int? YearOfPublication { get; set; }
        [MaxLength(1200)]
        public string? Description { get; set; }
        public string? Publisher { get; set; }
        public Uri? Cover { get; set; }
        public int? Pages { get; set; }
        [MaxLength(25)]
        public ICollection <int> AuthorIds { get; set; } = new List<int>();
        public ICollection<int> CategoryIds { get; set; } = new List<int>();
    }
}
  