using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class WhoBorrowBookDto
    {
        public string QRBookCode { get; set; }
        public Guid userId { get; set; }
    }
}
