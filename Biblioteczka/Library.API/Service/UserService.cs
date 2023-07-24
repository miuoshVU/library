using AutoMapper;
using Library.API.Interface;
using Library.API.Models;
using Library.CodeFirstDatabase.Entities;
using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly LibraryDbContext _dbContext;
        private readonly IAuthenticationService _authenticationService;
        public UserService(LibraryDbContext dbContext, IMapper mapper, IAuthenticationService authenticationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        public User CreateUser(UpdateUserDto dto)
        {
            var user = new User();
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Password = _authenticationService.CreatePassword(dto.Paswd);
            user.Avatar = dto.Avatar;
            user.Email = dto.Email;
            user.RemainingVotes = (int)dto.RemainingVotes;
            user.Role = dto.Role;

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user;
        }

        public bool DeleteUser(Guid id)
        {
            var user = _dbContext
               .Users
               .FirstOrDefault(b => b.Id == id);
            if (user is null) return false;
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
            return true;
        }

        public UserDto GetUserByEmail(string mail)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Email == mail);
            if (user is null) return null;

            return _mapper.Map<UserDto>(user);
        }

        public IEnumerable<UserDto> GetAllUser()
        {
            var users = _dbContext
                .Users
                .ToList();

            var usersDtos = new List<UserDto>();
            foreach (var user in users)
            {
                usersDtos.Add(_mapper.Map<User, UserDto>(user));
            }
            return usersDtos;
        }

        public bool UpdateUser(Guid id, UpdateUserDto dto)
        {
            var user = _dbContext
               .Users
               .FirstOrDefault(b => b.Id == id);
            if (user is null) 
                return false;

            if(dto.FirstName is not null)
                user.FirstName = dto.FirstName;
            if(dto.LastName is not null)
                user.LastName = dto.LastName;
            if(dto.Email is not null)
                user.Email = dto.Email;
            if(dto.Role is not null)
                user.Role = dto.Role;
            if(dto.Avatar is not null)
                user.Avatar = dto.Avatar;
            if(dto.RemainingVotes is not null)
                user.RemainingVotes = (int)dto.RemainingVotes;
            if(dto.Paswd is not null)
            {
                var pDto = new UpdatePasswordDto() { Paswd = dto.Paswd, UserId = user.Id };
                user.Password = _authenticationService.UpdatePassword(pDto);
            }

            if (dto.Borrows is not null)
                user.Borrows = dto.Borrows;

            _dbContext.SaveChanges();

            return true;
        }

        public async Task<User> GetUserById(Guid userId)
        {
            var user = await _dbContext
                .Users
                .Where(u => u.Id.Equals(userId))
                .FirstOrDefaultAsync();
            
            return user;
        }
    }
}
