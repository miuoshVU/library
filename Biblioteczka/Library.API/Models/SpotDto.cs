using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class SpotDto
    {
       public int id { get; set; }
        public string Name { get; set; }
        public string Building { get; set; }
        public int Floor { get; set; }
        public string? Description { get; set; }
        public string? Qr { get; set; }
        public int? bookCount { get; set; } = 0;
      
    }
}
