using Library.API.Interface;
using Library.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Library.Controllers
{
    [Route("/Library")]
    public class LibraryController : Controller
    {

        private readonly ILibraryService _libraryService;
        private readonly IBookService _bookService;
        private readonly ILibrarySearchService _librarySearchService;
        private readonly IBorrowService _borrowService;
        private readonly IReturnBookService _returnBookService;
        public LibraryController(ILibraryService libraryService,
            IBookService bookService, ILibrarySearchService librarySearchService,
            IBorrowService borrowService, IReturnBookService returnBookService)
        {

            _libraryService = libraryService;
            _bookService = bookService;
            _librarySearchService = librarySearchService;
            _borrowService = borrowService;
            _returnBookService = returnBookService;
        }

        [Authorize(Roles = "user, admin")]
        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetAll()
        {
            var books = await _bookService.GetAll();
            return Ok(books);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddBook([FromBody] AddNewBookDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var title = _bookService.Create(dto);
            return Created($"/Library/Book/{title}", null);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _bookService.Delete(id);
            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();

        }

        [Authorize(Roles = "admin")]
        [HttpPatch("{id}")]
        public ActionResult Update([FromBody] UpdateBookDto dto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _bookService.Update(id, dto);
            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok();
        }

        [Authorize(Roles = "user, admin")]
        [HttpGet("Search/{searchQuery}")]
        public async Task<ActionResult<List<BookDto>>> SearchBooks([FromRoute] string searchQuery)
        {
            var books = await _librarySearchService.Search(searchQuery);
            if (books is null)
            {
                return NoContent();
            }
            return Ok(books);
        }

        [Authorize(Roles = "user, admin")]
        [HttpGet("Category/Books/{categoryId}")]
        public async Task<ActionResult<List<BookDto>>> GetAllBooksFromCategory([FromRoute] int categoryId)
        {
            var books = await _bookService.GetBooksFromCategory(categoryId);
            if (books is null)
            {
                return NoContent();
            }

            return Ok(books);
        }

       // [Authorize(Roles = "user")]
        [HttpPost("Borrow")]
        public async Task<ActionResult<BorrowDto>> BorrowBook([FromBody] WhoBorrowBookDto who)
        {
            var borrow = await _borrowService.BorrowBook(who.QRBookCode, who.userId);
            if (borrow is null)
            {
                return NotFound();
            }

            return Ok(borrow);
        }

        [Authorize(Roles = "user")]
        [HttpPatch("ReturnBook")]
        public async Task<ActionResult<BorrowDto>> ReturnBook([FromBody] CheckWhoReturnBook whoreturn)
        {
            var returnBook = await _returnBookService.ReturnBook(whoreturn.userId, whoreturn.spotQrCode, whoreturn.QRBookCode);
            if (returnBook is null)
            {
                return NotFound();
            }

            return Ok(returnBook);
        }

        [Authorize(Roles = "user, admin")]
        [HttpGet("Check")]
        public async Task<ActionResult<bool>> CheckIfUserHasBook(Guid user, string BookQrCode)
        {
            var check = await _borrowService.CheckIfUserHasThisBook(user, BookQrCode);
            if (check == null)
            {
                return false;
            }

            return true;
        }

        [Authorize(Roles = "user, admin")]
        [HttpGet("/Spot/{bookId}")]
        public async Task<ActionResult<BookInstanceDto>> FindBookSpot([FromRoute] int bookId)
        {
            var boookSpot = await _bookService.FindBookSpot(bookId);
            if (boookSpot is null)
            {
                return NotFound();
            }

            return Ok(boookSpot);
        }
        
       // [Authorize(Roles = "user, admin")]
        [HttpGet("/Borrowed/{userId}")]
        public async Task<ActionResult<List<BookInstanceDto>>> ShowAllBorrowedBook([FromRoute] Guid userId)
        {
            var borrowedBooks = await _borrowService.ShowAllBorrowedBook(userId);
            if (borrowedBooks is null)
            {
                return NotFound();
            }

            return Ok(borrowedBooks);
        }

        //[Authorize(Roles = "user, admin")]
        [HttpPatch("RenewABook/{borrowId}")]
        public async Task<ActionResult<bool>> RenewABook([FromRoute] int borrowId)
        {
            var renew = await _borrowService.RenewABook(borrowId);
            if (renew == null)
            {
                return NotFound();
            }

            return Ok(renew);
        }
        //[Authorize(Roles = "user, admin")]
        [HttpGet("HowManyDaysToReturnBook/{borrowId}")]
        public async Task<ActionResult<int>> HowManyDaysLeft([FromRoute] int borrowId)
        {
            var howManyDays = await _borrowService.HowManyDaysLeft(borrowId);
            if (howManyDays == null)
            {
                return NotFound();
            }

            return Ok(howManyDays);
        }
    }
}

