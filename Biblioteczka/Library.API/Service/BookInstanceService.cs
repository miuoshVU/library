using AutoMapper;
using Library.API.Interface;
using Library.API.Models;
using Library.CodeFirstDatabase.Entities;
using Library.CodeFirstDatabase.Enum;
using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Service
{
    public class BookInstanceService : IBookInstancesService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;
        public BookInstanceService(LibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public bool AddBookInstance(NewBookInstanceDto bookInstance)
        {
            var instance = new BookInstance()
            {
                OwnerName = bookInstance.Owner,
                Status = (Status)bookInstance.Status
            };
            var book = _dbContext
                .Books
                .FirstOrDefault(b => b.Id == bookInstance.bookID);
            instance.Book = book;

            var spot = _dbContext
                .Spots
                .FirstOrDefault(c => c.Id == bookInstance.spotID);
            instance.Spot = spot;

            instance.QR = "";

            _dbContext.Book_Instances.Add(instance);
            _dbContext.SaveChanges();
            
            return true;
        }

        public List<BookInstanceDto> GetAll()
        {
            var instances = _dbContext
                .Book_Instances
                .Include(i => i.Book)
                .ToList();

            var instancesDto = _mapper.Map<List<BookInstance>, List<BookInstanceDto>>(instances);

            return instancesDto;
        }

        public List<BookInstanceDto> GetAllBook(int bookId)
        {
            
            var instances = _dbContext
               .Book_Instances
               .Include(i => i.Spot)
               .Include(i => i.Book)
               .Where(i => i.Book.Id == bookId)
               .Where(i => i.Status == Status.Available)
               .ToList();

            var instancesDto = _mapper.Map<List<BookInstance>, List<BookInstanceDto>>(instances);

            return instancesDto;
        }

        public bool RemoveBookInstance(int bookInstanceId)
        {
            var bookInstance = _dbContext
                .Book_Instances
                .FirstOrDefault(i => i.Id == bookInstanceId);
            if(bookInstance == null)
                return false;

            _dbContext.Remove(bookInstance);
            _dbContext.SaveChanges();

            return true;
        }

        public bool UpdateBookInstance(int bookInstanceId, UpdateBookInstanceDto bookInstance)
        {
            var instance = _dbContext
                .Book_Instances
                .FirstOrDefault(i => i.Id == bookInstanceId);
            if (instance == null)
                return false;

            if(bookInstance.bookID is not null)
            {
                var book = _dbContext
                .Books
                .FirstOrDefault(b => b.Id == bookInstance.bookID);
                instance.Book = book;
            }
            
            if(bookInstance.spotID is not null)
            {
                var spot = _dbContext
                .Spots
                .FirstOrDefault(c => c.Id == bookInstance.spotID);
                instance.Spot = spot;
            }
            if (bookInstance.Owner is not null)
                instance.OwnerName = bookInstance.Owner;
            if (bookInstance.Status is not null)
                instance.Status = (CodeFirstDatabase.Enum.Status)bookInstance.Status;

            _dbContext.SaveChanges();

            return true;
        }

        public async Task<BookInstance> FindBookInstanceByQrCode(string bookQrCode)
        {
            var searchbookInstance = await _dbContext
                .Book_Instances
                .Where(c => c.QR.Contains(bookQrCode)
                            && c.Status == CodeFirstDatabase.Enum.Status.Borrowed)
                .FirstOrDefaultAsync();
            
            return searchbookInstance;
        }
    }
}
