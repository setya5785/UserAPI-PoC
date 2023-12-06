using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UserAPI.Application;
using UserAPI.Domain;

namespace UserAPI.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetUsersAsync(string UserId)
        {
            if (UserId.ToUpper() == "ALL" ) {
                // get all data
                return _dbContext.Users;
            }
            else
            {
                List<User> returnValue = new List<User>();
                int n;
                if (int.TryParse(UserId, out n))
                {
                    var existUser =  await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == n);
                    if(existUser != null)
                    {
                        returnValue.Add(existUser);
                    }

                    return returnValue;
                }
                else
                {
                    throw new Exception("Enter 'All' or valid integer UserId!");
                }
            }

        }

        public async Task<(int, string)> SetUserAsync(User model)
        {
            var userIdExist = await _dbContext.Users.FirstOrDefaultAsync(c => c.UserId == model.UserId);            
            var usernameExist = await _dbContext.Users.FirstOrDefaultAsync(c => c.Username == model.Username);            
            var hashedPassword = HashPassword(model.Password);

            if (userIdExist != null)
            {
                userIdExist.NamaLengkap = model.NamaLengkap;
                if(usernameExist !=null && userIdExist.UserId != usernameExist.UserId)
                {
                    return (0, "Username already taken!");
                }
                userIdExist.Username = model.Username;
                if(!(userIdExist.Password == hashedPassword))
                {
                    userIdExist.Password = hashedPassword;
                }

                userIdExist.Status = model.Status;

                await _dbContext.SaveChangesAsync();

                return (1, "User data updated!");
            }

            if (usernameExist != null)
            {
                return (0, "Username already taken!");
            }

            User user = new User
            {
                NamaLengkap = model.NamaLengkap,
                Username = model.Username,
                Password = hashedPassword,
                Status = model.Status,
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return (1, "User created successfully!");
        }

        public async Task<(int, string)> DeleteUserAsync(int userId)
        {
            var userExist = await _dbContext.Users.FindAsync(userId);
            if (userExist != null)
            {
                _dbContext.Users.Remove(userExist);
                await _dbContext.SaveChangesAsync();

                return (1, $"User {userId} deleted successfully!");
            }
            else
            {
                return (0, "User not found in database!");
            }
        }

        private string HashPassword(string password)
        {
            // hash this password for storing
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }


    }
}
