using Library.API.Models;
using Library.CodeFirstDatabase.Entities;

namespace Library.API.Interface
{
    public interface IAuthenticationService
    {
        public bool CheckPassword(string emali, string paswd);
        public Password CreatePassword(string paswd);
        public bool DeletePassword(Guid id);
        public Password UpdatePassword(UpdatePasswordDto dto);
    }
}