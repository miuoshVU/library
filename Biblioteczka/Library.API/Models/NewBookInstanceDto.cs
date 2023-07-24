using Library.CodeFirstDatabase.Enum;

namespace Library.API.Models
{
    public class NewBookInstanceDto
    {
        public int? bookID { get; set; }
        public int? spotID { get; set; }
        public string? Owner { get; set; }        
        public Status? Status { get; set; }
    }
}
