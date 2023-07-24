namespace Library.API.Interface;

public interface IQuickBorrowOrReturn
{
    Task<bool> QuickBorrowOrReturnBook(Guid user, string spotQr, string bookQr);
}