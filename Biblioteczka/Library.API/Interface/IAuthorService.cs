using Library.API.Models;
using Library.CodeFirstDatabase.Entities;

namespace Library.API.Interface
{
    public interface IAuthorService
    {
        IEnumerable<AuthorDto> GetAllAuthors();
        bool UpdateAuthor(int id, AuthorDto authorDto);
        bool DeleteAuthor(int id);
        Author CreateAuthor(AuthorDto authorDto);
        IEnumerable<AuthorDto> GetAuthorByName(string name);
    }
}
