using Library.CodeFirstDatabase.Entities;

namespace Library.API.Interface;

public interface IReturnBookService
{
    Task<Borrow> ReturnBook(Guid userId, string spotQrCode, string bookQrCode);
}