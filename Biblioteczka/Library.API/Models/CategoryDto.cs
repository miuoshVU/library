using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class CategoryDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public Uri? Cover { get; set; } = new Uri("https://lexliber.pl/userdata/public/gfx/15444/default.jpg");
    }
}
