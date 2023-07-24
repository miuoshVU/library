using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.CodeFirstDatabase.Entities;

namespace Library.API.Models
{
    public class BookDto
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ISBN { get; set; }
        public int YearOfPublication { get; set; }
        public string? Description { get; set; }
        public string? Publisher { get; set; }
        public Uri? Cover { get; set; } = new Uri("https://lexliber.pl/userdata/public/gfx/15444/default.jpg");
        public int? Pages { get; set; }
      
        public List<CategoryDto> Categories { get; set; }
        public List<AuthorDto> Authors { get; set; }
    }
}
