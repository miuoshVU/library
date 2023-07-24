using Library.API.Interface;
using Library.API.Models;
using Library.CodeFirstDatabase.Entities;
using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Service
{
    public class VotingService : IVotingService
    {
        private readonly LibraryDbContext _dbContext;
        public VotingService(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool RestartVoting()
        {
            var users = _dbContext
                .Users
                .Include(u => u.ProposedBooks)
                .ToList();
            foreach (var user in users)
            {
                user.RemainingVotes = 5;
                user.ProposedBooks = new List<ProposedBook>();
            }

            _dbContext.SaveChanges();
            return true;
        }

        public bool Vote(VoteUserDto user)
        {
            var oldUser = _dbContext
                .Users
                .Include(u => u.ProposedBooks)
                .FirstOrDefault(u => u.Id == user.UserId);

            if (oldUser is null)
                return false;

            var remainingVotes = oldUser.RemainingVotes;
            //   foreach (var vote in oldUser.ProposedBooks)
            //   {
            //  //     remainingVotes++;
            //       var book = _dbContext
            //           .Proposed_Books
            //           .Include(p => p.Users)
            //           .FirstOrDefault(b => b.Id == vote.Id);
            //       oldUser.ProposedBooks.Remove(book);
            //   }
            
            var books = _dbContext
                    .Proposed_Books
                    .Include(p => p.Users)
                    .Where(b => b.Users.Contains(oldUser));
                    
            foreach(var b in books)
            {
                oldUser.ProposedBooks.Remove(b);
                b.Points--;
                remainingVotes++;
            }
            _dbContext.SaveChanges();
            
            foreach(var vote in user.ProposedBooksId)
            {
                if (remainingVotes <= 0)
                    break;

                remainingVotes--;
                var book = _dbContext
                    .Proposed_Books
                    .Include(p => p.Users)
                    .FirstOrDefault(b => b.Id == vote);
                
                oldUser.ProposedBooks.Add(book);
                book.Points++;
            }
            oldUser.RemainingVotes = remainingVotes;

            _dbContext.SaveChanges();
            return true;
        }
    }
}
