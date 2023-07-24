using AutoMapper;
using Library.API.Interface;
using Library.API.Models;
using Library.CodeFirstDatabase.Entities;
using Library.CodeFirstDatabase.Enum;
using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Service
{
    public class BorrowService : IBorrowService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;
        public BorrowService(LibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        //Borrow Service
        public async Task<AddNewBorrow> BorrowBook(string QRBookCode, Guid userId)
        {

            var searchbookInstance = await _dbContext
                .Book_Instances
                .Where(c => c.QR.Contains(QRBookCode)
                            && c.Status == CodeFirstDatabase.Enum.Status.Available)
                .FirstOrDefaultAsync();
            if (searchbookInstance == null)
            {
                return null;
            }
            var user = await _dbContext
                .Users
                .Where(u => u.Id.Equals(userId))
                .FirstOrDefaultAsync();

            var borrow = new Borrow();
            borrow.User = user;
            borrow.BookInstance = searchbookInstance ;
            borrow.BorrowDate = DateTime.UtcNow;
            borrow.ReturnDate = DateTime.UtcNow.AddDays(31);
            borrow.Status = Status.Borrowed;

           await _dbContext.Borrows.AddAsync(borrow);
           await _dbContext.SaveChangesAsync();
            
            searchbookInstance.Status = Status.Borrowed;
            await _dbContext.SaveChangesAsync();

            var addNewBorrowDto = new AddNewBorrow();
            addNewBorrowDto.user = borrow.User;
            addNewBorrowDto.bookInstance = borrow.BookInstance; 
            addNewBorrowDto.BorrowDate = borrow.BorrowDate;
            addNewBorrowDto.ReturnDate = borrow.ReturnDate;
            addNewBorrowDto.Status = borrow.Status;

             return addNewBorrowDto;
            
        }
        //Show All Books Borrowed By User
        public async Task<List<BookInstanceDto>> ShowAllBorrowedBook(Guid userId)
        {
            var borrowed = await _dbContext
                .Borrows
                .Include(b => b.User)
                .Include(b => b.BookInstance)
                .Where(b => b.UserId == userId && (b.Status == Status.Borrowed || b.Status == Status.Hold))
                .OrderBy(b => b.ReturnDate)
                .Select(b => b.BookInstance.Id)
                .ToListAsync();
                
            var books = await _dbContext
                .Book_Instances
                .Include(b => b.Book)
                .Include(b => b.Spot)
                .Include(b=>b.Borrows)
                .Where(b => borrowed.Contains(b.Id))
                .ToListAsync();

            var borrowedBooks =_mapper.Map<List<BookInstanceDto>>(books);

            return borrowedBooks;
        }

        public async Task<bool> CheckIfUserHasThisBook(Guid userId, string QrBookCode)
        {
            var borrowed = await _dbContext
                .Borrows
                .Include(b => b.User)
                .Include(b => b.BookInstance)
                .Where(b => b.UserId == userId && b.BookInstance.QR.Contains(QrBookCode))
                .Select(b => b.BookInstanceId)
                .FirstOrDefaultAsync();
            if (borrowed == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> RenewABook(int borrowId)
        {
            var borrow = await _dbContext
                .Borrows
                .Where(b => b.Id.Equals(borrowId) && (b.Status == Status.Borrowed || b.Status==Status.Hold))
                .FirstOrDefaultAsync();

            if (borrow == null)
            {
                return false;
            }
            else
            {
                    borrow.ReturnDate = DateTime.UtcNow.AddDays(20);
                    borrow.Status = Status.Hold;
                    await _dbContext.SaveChangesAsync();
                    return true;
            }
        }

        public async Task<int> HowManyDaysLeft(int borrowId)
        {
            var borrow = await _dbContext
                .Borrows
                .Where(b => b.Id.Equals(borrowId) && (b.Status == Status.Borrowed||b.Status==Status.Hold))
                .Select(b=>b.ReturnDate)
                .FirstOrDefaultAsync();
            var returnDate = borrow;
            var now = DateTime.UtcNow;
            var howManyDays = borrow.Subtract(now).Days;
            
            return howManyDays;
        }
    }
}
