using AutoMapper;
using Library.API.Interface;
using Library.API.Models;
using Library.CodeFirstDatabase.Entities;
using Library.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Library.API.Service
{    
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly LibraryDbContext _dbContext;
        public AuthenticationService(IMapper mapper, LibraryDbContext libraryDbContext)
        {
            _mapper = mapper;
            _dbContext = libraryDbContext;
        }
        public Password CreatePassword(string paswd)
        {
            string salt = ComputeSha512Hash(new Random().Next().ToString());
            int rounds = new Random().Next(1000);

            Password password = new Password()
            {
                Id = Guid.NewGuid(),
                Salt = salt,
                Rounds = rounds,
                Hash = HashPassword(paswd, salt, rounds)
            };

            _dbContext.Passwords.Add(password);
            _dbContext.SaveChanges();
            return password;
        }
        private static string ComputeSha512Hash(string rawData)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                for (int i = 0; i < bytes.Length; i++)
                {
                    if (bytes[i] == 0)
                        bytes[i] = 1;
                }
                return Encoding.Default.GetString(bytes);
                //return bytes.ToString();
            }
        }
        private static string HashPassword(string paswd, string salt, int rounds)
        {
            for (int i=0; i<rounds; i++)
            {
                paswd = salt + paswd;
                paswd = ComputeSha512Hash(paswd);
            }
            return paswd;
        }
        public bool CheckPassword(string mail, string paswd)
        {
            var usr = _dbContext
                .Users
                .Include(u => u.Password)
                .FirstOrDefault(u => u.Email == mail);
            if (usr is null) 
                return false;
            
            return usr.Password.Hash == HashPassword(paswd, usr.Password.Salt, usr.Password.Rounds);
        }

        public bool DeletePassword(Guid id)
        {
            var password = _dbContext
                .Passwords
                .FirstOrDefault(b => b.Id == id);
            if (password is null) return false;
            _dbContext.Passwords.Remove(password);
            _dbContext.SaveChanges();
            return true;
        }

        public Password UpdatePassword(UpdatePasswordDto dto)
        {
            var password = _dbContext
               .Passwords
               .Include(u => u.User)
               .FirstOrDefault(u => u.User.Id == dto.UserId);

            if (password is null)
                return null;

            var newPassword = CreatePassword(dto.Paswd);

            password.Hash = newPassword.Hash;
            password.Rounds = newPassword.Rounds;
            password.Salt = newPassword.Salt;

            _dbContext.SaveChanges();

            return password;
        }
    }
}
