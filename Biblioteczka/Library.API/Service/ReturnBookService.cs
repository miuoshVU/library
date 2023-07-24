using Library.API.Interface;
using Library.CodeFirstDatabase.Entities;
using Library.CodeFirstDatabase.Enum;
using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Service
{
    public class ReturnBookService : IReturnBookService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IBookInstancesService _bookInstances;
        private readonly IUserService _userService;
        private readonly ISpotService _spotService;
        public ReturnBookService(LibraryDbContext dbContext, IBookInstancesService bookInstances,IUserService userService, ISpotService spotService)
        {
            _dbContext = dbContext;
            _bookInstances = bookInstances;
            _userService = userService;
            _spotService = spotService;
        }
        public async Task<Borrow> ReturnBook(Guid userId, string spotQrCode, string bookQrCode)
        {
            var searchbookInstance = await _bookInstances.FindBookInstanceByQrCode(bookQrCode);

            var user = await _userService.GetUserById(userId);

            var searchBorrowEntry = await _dbContext
                .Borrows
                .Where(b => b.UserId.Equals(user.Id)
                            && b.BookInstanceId.Equals(searchbookInstance.Id) 
                            && b.Status == Status.Borrowed)
                .FirstOrDefaultAsync();

            var searchspot = await _spotService.FindSpotByQr(spotQrCode);

            var bookreturned = await _dbContext
                .Borrows
                .Where(b => b.Id.Equals(searchBorrowEntry.Id))
                .FirstOrDefaultAsync();

            bookreturned.ReturnDate = DateTime.UtcNow;
            bookreturned.Status = Status.Returned;
            searchbookInstance.Status = Status.Available;
            searchbookInstance.Spot = searchspot;

            await _dbContext.SaveChangesAsync();

            return bookreturned;
        }
    }
}
