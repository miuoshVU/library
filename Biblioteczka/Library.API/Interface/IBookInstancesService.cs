using Library.API.Models;
using Library.CodeFirstDatabase.Entities;

namespace Library.API.Interface
{
    public interface IBookInstancesService
    {
        public List<BookInstanceDto> GetAll();
        public List<BookInstanceDto> GetAllBook(int bookId); //instancje danej książki
        public bool AddBookInstance(NewBookInstanceDto bookInstance);
        public bool RemoveBookInstance(int bookInstanceId);
        public bool UpdateBookInstance(int bookInstanceId, UpdateBookInstanceDto bookInstance);
        public Task<BookInstance> FindBookInstanceByQrCode(string bookQrCode);
    }
}
