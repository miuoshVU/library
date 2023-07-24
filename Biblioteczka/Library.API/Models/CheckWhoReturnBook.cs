using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class CheckWhoReturnBook
    {
        public Guid userId { get; set; }
        public string QRBookCode { get; set; }
        public string spotQrCode { get; set; }
    }
}
