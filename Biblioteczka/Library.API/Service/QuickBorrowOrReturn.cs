using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.API.Interface;
using Library.API.Models;
using Library.CodeFirstDatabase.Entities;

namespace Library.API.Service
{
    public class QuickBorrowOrReturn : IQuickBorrowOrReturn
    {
        private readonly IBorrowService _borrowService;
        private readonly IReturnBookService _returnBookService;

        public QuickBorrowOrReturn(IBorrowService borrowService,IReturnBookService returnBookService)
        {
            _borrowService = borrowService;
            _returnBookService = returnBookService;
        }

        public async Task<bool>  QuickBorrowOrReturnBook(Guid user, string spotQr, string bookQr)
        {
            var check = await _borrowService.CheckIfUserHasThisBook(user, bookQr);
            if (check is true)
            {
                var returnBook = await _returnBookService.ReturnBook(user, spotQr,bookQr );
                return true;
            }
            else
            {
                var borrowBook = await _borrowService.BorrowBook(bookQr, user);
                var addNewBorrowDto = new AddNewBorrow();
                return false;
            }
        }
    }
}
