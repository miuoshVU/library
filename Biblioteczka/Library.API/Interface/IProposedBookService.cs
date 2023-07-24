using Library.API.Models;
using Library.CodeFirstDatabase.Entities;

namespace Library.API.Interface
{
    public interface IProposedBookService
    {
        IEnumerable<ProposedBookDto> GetAllProposedBook();
        bool UpdateProposedBook(int id, UpdateProposedBookDto proposedBooksDto);
        bool DeleteProposedBook(int id);
        ProposedBook CreateProposedBook(ProposedBookDto proposedBookDto);
    }
}
