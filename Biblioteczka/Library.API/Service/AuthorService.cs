using AutoMapper;
using Library.API.Interface;
using Library.API.Models;
using Library.CodeFirstDatabase.Entities;
using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuthorService(LibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Author CreateAuthor(AuthorDto authorDto)
        {
            var author = new Author();
            author = _mapper.Map<Author>(authorDto);

            _dbContext.Add(author);
            _dbContext.SaveChanges();

            return author;

        }

        public bool DeleteAuthor(int id)
        {
            var author = _dbContext
                .Authors
                .FirstOrDefault(a => a.Id == id);
            
            if(author is null) return false;
            
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();

            return true;
        }

        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            var authors = _dbContext
                .Authors
                .Include(a => a.Books)
                .ToList();

            if (authors is null)
                authors = new List<Author>();

            var authorsDtos = new List<AuthorDto>(); // _mapper.Map<List<AuthorDto>>(authors);
            
            foreach(var author in authors)
            {
                var a = _mapper.Map<AuthorDto>(author);
                a.bookCount = author.Books.Count;
                authorsDtos.Add(a);
            }
            
            return authorsDtos;
        }

        public IEnumerable<AuthorDto> GetAuthorByName(string name)  //Nieużywane
        {
            var authors = _dbContext
                .Authors
                .Where(a =>
                        a.FirstName.Contains(name, StringComparison.CurrentCultureIgnoreCase) ||
                        a.LastName.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                .ToList();

            if (authors is null)
                authors = new List<Author>();

            var authorDtos = _mapper.Map<List<AuthorDto>>(authors);
            return authorDtos;
        }

        public bool UpdateAuthor(int id, AuthorDto authorDto)
        {
            var Author = _dbContext
                .Authors
                .FirstOrDefault(b => b.Id == id);

            if (Author is null)
                return false;
            
            Author.FirstName = authorDto.FirstName;
            Author.LastName = authorDto.LastName;

            _dbContext.SaveChanges();
            return true;
        }
    }
}
