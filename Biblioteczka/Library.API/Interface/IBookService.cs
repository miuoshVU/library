using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.API.Models;

namespace Library.API.Interface
{
    public interface IBookService
    {
        Task<List<BookDto>> GetBySearchAuthor(string? authorName);
        Task<List<BookDto>> GetBookBySearchCategory(string category);
        Task<List<BookDto>> GetBookBySearchTitle(string searchQuery);
        Task<List<BookDto>> GetBooksFromCategory(int categoryId);
        Task<IEnumerable<BookDto>> GetAll();
        string Create(AddNewBookDto dto);
       // BookDto GetByIsbn(string isbn);
        bool Update(int id, UpdateBookDto dto);
        bool Delete(int id);
        Task<List<BookInstanceSpotInfoDto>> FindBookSpot(int bookId);

    }
}
