using AutoMapper;
using Library.API.Interface;
using Library.API.Models;
using Library.CodeFirstDatabase.Entities;
using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Service
{
    public class LibraryService : ILibraryService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorService _authorService;
        private readonly ICategoryService _categoryService;
        public LibraryService(LibraryDbContext dbContext, IMapper mapper, IAuthorService authorService, ICategoryService category)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorService = authorService;
            _categoryService = category;
        }

    }
}

