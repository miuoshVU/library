using AutoMapper;
using Library.API.Interface;
using Library.API.Models;
using Library.CodeFirstDatabase.Entities;
using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Service
{
    public class ProposedBookService : IProposedBookService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorService _authorService;
        public ProposedBookService(LibraryDbContext libraryDataContext, IMapper mapper, IAuthorService authorService)
        {
            _dbContext = libraryDataContext;
            _mapper = mapper;
            _authorService = authorService;
        }

        public ProposedBook CreateProposedBook(ProposedBookDto proposedBookDto)
        {
            var proposedBook = new ProposedBook();
            //proposedBook = _mapper.Map<ProposedBook>(proposedBookDto);

            proposedBook.Title = proposedBookDto.Title;
            proposedBook.UrlLink = proposedBookDto.UrlLink;
            proposedBook.Points = proposedBookDto.Points;
            proposedBook.Authors = proposedBookDto.Authors;
            proposedBook.Categories = proposedBookDto.Categories;
            proposedBook.Cover = proposedBookDto.Cover;
            

            _dbContext.Add(proposedBook);
            _dbContext.SaveChanges();

            return proposedBook;
        }

        public bool DeleteProposedBook(int id)
        {
            var proposedBook = _dbContext
                .Proposed_Books
                .FirstOrDefault(p => p.Id == id);

            if(proposedBook is null) return false;
        
            _dbContext.Remove(proposedBook);
            _dbContext.SaveChanges();

            return true;
        }

        public IEnumerable<ProposedBookDto> GetAllProposedBook()
        {
            var proposedBooks = _dbContext
                .Proposed_Books
                .ToList();

            if (proposedBooks is null)
                proposedBooks = new List<ProposedBook>();

            var proposedBooksDto = _mapper.Map<List<ProposedBook>, List<ProposedBookDto>>(proposedBooks);
                
            return proposedBooksDto;
        }

        public bool UpdateProposedBook(int id, UpdateProposedBookDto proposedBookDto)
        {
            var proposedBook = _dbContext
                .Proposed_Books
                .FirstOrDefault(p => p.Id == id);

            if(proposedBook is null)
                return false;

            if(proposedBookDto.Title is not null)
                proposedBook.Title = proposedBookDto.Title;
            if(proposedBookDto.UrlLink is not null)
                proposedBook.UrlLink = proposedBookDto.UrlLink;
            if (proposedBookDto.Points is not null)    
                proposedBook.Points = (int)proposedBookDto.Points;
            if (proposedBookDto.Authors is not null)
                proposedBook.Authors = proposedBookDto.Authors;
            if(proposedBookDto.Categories is not null)
                proposedBook.Categories = proposedBookDto.Categories;
            if (proposedBookDto.Cover is not null)
                proposedBook.Cover = proposedBookDto.Cover;
            
            
           
            _dbContext.SaveChanges();

            return true;
        }
    }
}
