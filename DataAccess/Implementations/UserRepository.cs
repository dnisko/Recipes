using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly RecipeDbContext _context;

        public UserRepository(RecipeDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> LoginAsync(string username, string hashPassword)
        {
            var user = await _context.Users.FindAsync(username, hashPassword);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with username: {username} is not found.");
            }
            return user;
        }

        public async Task<IEnumerable<User>> GetByUsernameAsync(string username)
        {
            var users = await _context.Users
                .Where(u => u.Username.Contains(username)).ToListAsync();
            if (users == null)
            {
                throw new KeyNotFoundException($"User with username: {username} is not found.");
            }
            return users;
        }

        public async Task<User> GetSingleUserByUsernameAsync(string username)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                throw new KeyNotFoundException($"User with username '{username}' not found.");

            return user;
        }

        public async Task<User> CheckPasswordAsync(string hashPassword)
        {
            try
            {
                var checkPassword = await _context.Users.FirstOrDefaultAsync(u => u.PasswordHash == hashPassword);
                return checkPassword;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> Register(User registerUser)
        {
            //Console.WriteLine("Starting user registration process.");
            try
            {
                _context.Users.Add(registerUser);
                //Console.WriteLine("Calling save changes.");
                await _context.SaveChangesAsync();
                //Console.WriteLine("User registration success.");
                return registerUser;
            }
            catch (DbUpdateException dbEx)
            {
                // Log the inner exception or additional details
                throw new Exception("Database update error: " + dbEx.InnerException?.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
