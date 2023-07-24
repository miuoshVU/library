using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library.API.Interface;
using Library.API.Models;
using Library.CodeFirstDatabase.Entities;
using Library.Entities;
using Microsoft.EntityFrameworkCore;


namespace Library.API.Service
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorService _authorService;
        private readonly ICategoryService _categoryService;
        public BookService(LibraryDbContext dbContext, IMapper mapper, IAuthorService authorService, ICategoryService categoryService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorService = authorService;
            _categoryService = categoryService;
        }

        //Searching
        public async Task<List<BookDto>> GetBySearchAuthor(string? authorName)
        {
           var matchingAuthors = await _dbContext
                .AuthorView
                .Where(a => a.NameAndLastName.ToLower().Contains(authorName.ToLower()))
                .Select(a => a.Id)
                .ToListAsync();


            var books = await _dbContext
                .Books
                .Include(b => b.Categories)
                .Include(b => b.Authors)
                .Where(b => b.Authors.Any(a => matchingAuthors.Contains(a.Id)))
                .ToListAsync();

            var booksDtos = _mapper.Map<List<BookDto>>(books);
            return booksDtos;
            
        }
        public async Task<List<BookDto>> GetBookBySearchCategory(string category)
        {
            var matchingCategory = await _dbContext
                .CategoryView
                .Where(c => c.Name.ToLower().Contains(category.ToLower()))
                .Select(c => c.Id)
                .ToListAsync();

            var books = await _dbContext
                .Books
                .Include(b => b.Authors)
                .Include(b => b.Categories)
                .Where(b => b.Categories.Any(c => matchingCategory.Contains(c.Id)))
                .ToListAsync();

            var booksDtos = _mapper.Map<List<BookDto>>(books);
            return booksDtos;
        }

        public async Task<List<BookDto>> GetBookBySearchTitle(string searchQuery)
        {
            var matchingBooks = await _dbContext
                .BookView
                .Where(b => b.Title.ToLower().Contains(searchQuery.ToLower()))
                .Select(c => c.Id)
                .ToListAsync();

            var books = await _dbContext
                .Books
                .Include(b => b.Categories)
                .Include(b => b.Authors)
                .Where(b => matchingBooks.Contains(b.Id))
                .ToListAsync();

            var booksDtos = _mapper.Map<List<BookDto>>(books);
            return booksDtos;
        }
       

        //Get all books
        public async Task<IEnumerable<BookDto>> GetAll()
        {
            var books = await _dbContext
                .Books
                .Include(b => b.Authors)
                .Include(b => b.Categories)
                .ToListAsync();

            var booksDtos = _mapper.Map<List<BookDto>>(books);
            return booksDtos;
        }

        //Get all books from category
        public  async Task<List<BookDto>> GetBooksFromCategory(int categoryId)
        {
            var matchingCategory =  await _dbContext
                .CategoryView
                .Where(c => c.Id == categoryId)
                .Select(c => c.Id)
                .ToListAsync();

            var books = await _dbContext
                .Books
                .Include(b => b.Authors)
                .Include(b => b.Categories)
                .Where(b => b.Categories.Any(c => matchingCategory.Contains(c.Id)))
                .ToListAsync();

            var booksDtos = _mapper.Map<List<BookDto>>(books);
            return booksDtos;
        }
        //Create book
        public string Create(AddNewBookDto dto)
        {
            var book = new Book();
            var authors = new List<Author>();
            foreach (var authorId in dto.AuthorIds)
            {
                var result =  _dbContext
                    .Authors
                    .FirstOrDefault(a => a.Id == authorId);
                if (result is null)
                    throw new Exception("Wrong AuthorId");
 
                authors.Add(result);
            }

            var categories = new List<Category>();
            foreach (var categoryId in dto.CategoryIds)
            {
                var result = _dbContext
                    .Categories
                    .FirstOrDefault(c => c.Id == categoryId);

                if (result is null)
                    throw new Exception("Wrong CategoryId");

                categories.Add(result);
            }

            book.Title = dto.Title;
            book.Authors = authors;
            book.ISBN = dto.ISBN;
            book.YearOfPublication = dto.YearOfPublication;
            book.Categories = categories;
            book.Description = dto.Description;
            book.Cover = dto.Cover;
            book.Publisher = dto.Publisher;
            book.Pages = dto.Pages;

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

            return book.Title;
        }

        //Delete book
        public bool Delete(int id)
        {
            var book = _dbContext
                .Books
                .FirstOrDefault(b => b.Id == id);
            if (book is null) return false;
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            return true;
        }

        //Update book
        public bool Update(int id, UpdateBookDto dto) 
        {
            var book = _dbContext
                .Books
                .Include(b => b.Authors)
                .Include(b => b.Categories)
                .FirstOrDefault(b => b.Id == id);

            if (book is null)
                return false;

            var authors = new List<Author>();
            foreach (var authorId in dto.AuthorIds)
            {
                var result = _dbContext
                    .Authors
                    .FirstOrDefault(a => a.Id == authorId);

                if (result != null)
                    authors.Add(result);
            }
            
            var categories = new List<Category>();
            foreach (var categoryId in dto.CategoryIds)
            {
                var result = _dbContext
                    .Categories
                    .FirstOrDefault(c => c.Id == categoryId);
                if (result != null)
                    categories.Add(result);
            }

            if(dto.Title is not null)
                book.Title = dto.Title;
            if(dto.AuthorIds is not null)
                book.Authors = authors;
            if(dto.ISBN is not null)
                book.ISBN = dto.ISBN;
            if(dto.YearOfPublication is not null)
                book.YearOfPublication = dto.YearOfPublication;
            if(dto.CategoryIds is not null)
                book.Categories = categories;
            if(dto.Description is not null)
                book.Description = dto.Description;
            if(dto.Cover is not null)
                book.Cover = dto.Cover;
            if(dto.Publisher is not null)
                book.Publisher = dto.Publisher;
            if(dto.Pages is not null)
                book.Pages = dto.Pages;

            _dbContext.SaveChanges();
            return true;
        }

        //Get Current Book Spot
        public async Task<List<BookInstanceSpotInfoDto>> FindBookSpot(int bookId)
        {
            var matchingSpotAndStatus = await _dbContext
                .Book_Instances
                .Include(b=>b.Book)
                .Include(m=>m.Spot)
                .Where(b => bookId == b.Book.Id)
                .Select(m=>new BookInstanceSpotInfoDto
                {
                    Name = m.Spot.Name, 
                    Floor =m.Spot.Floor, 
                    Building = m.Spot.Building,
                    Status = m.Status
                })
                .ToListAsync();
            
           var matchingSpotAndStatusDtos = _mapper.Map<List<BookInstanceSpotInfoDto>>(matchingSpotAndStatus);
            return matchingSpotAndStatusDtos;
        }
    }
}