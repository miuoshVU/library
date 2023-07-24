using Library.API.Models;
using Library.CodeFirstDatabase.Entities;

namespace Library.API.Interface
{
    public interface IUserService
    {
        public User CreateUser(UpdateUserDto dto);
        public IEnumerable<UserDto> GetAllUser();
        public bool UpdateUser(Guid id, UpdateUserDto dto);
        public bool DeleteUser(Guid id);
        public UserDto GetUserByEmail(string mail);
        public Task<User> GetUserById(Guid userId);
    }
}