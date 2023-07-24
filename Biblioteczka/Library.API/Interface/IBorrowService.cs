using Library.API.Models;
using Library.CodeFirstDatabase.Entities;

namespace Library.API.Interface;

public interface IBorrowService
{
    public Task<AddNewBorrow> BorrowBook(string QRBookCode, Guid userId);
    public Task<List<BookInstanceDto>> ShowAllBorrowedBook(Guid userId);
    public Task<bool> CheckIfUserHasThisBook(Guid userId, string QrBookCode);
    public Task<bool> RenewABook(int borrowId);
    public Task<int> HowManyDaysLeft(int borrowId);

}