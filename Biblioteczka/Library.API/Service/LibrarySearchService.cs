using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.API.Interface;
using Library.API.Models;

namespace Library.API.Service
{
    public class LibrarySearchService : ILibrarySearchService
    {
        private readonly IBookService _bookService;

        public LibrarySearchService(IBookService bookService)
        {
            _bookService = bookService;
        }


        public async Task<IEnumerable<BookDto>> Search(string searchPhrase)
        {
            var authors = await _bookService.GetBySearchAuthor(searchPhrase);
            var category = await _bookService.GetBookBySearchCategory(searchPhrase);
            var books = await _bookService.GetBookBySearchTitle(searchPhrase);

           
            
            return books.Concat(category).Concat(authors);
        }
    }
}
