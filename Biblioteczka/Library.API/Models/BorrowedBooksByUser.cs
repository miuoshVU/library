using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class BorrowedBooksByUser
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Uri? Cover { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public List<AuthorDto> Authors { get; set; }
        public List<BookInstanceDto>BookInstances { get; set; }
    }
}
