using Microsoft.EntityFrameworkCore;
using SampleCommerce.Context;
using SampleCommerce.Models;

namespace SampleCommerce.Repositories
{
    public class UserRepo : BaseRepo<User, Guid>, IRepoWrite<User>, IRepoRead<Guid, User>
    {
        public UserRepo(EcommerceDbContext context) : base(context) { }

        public bool EmailExists(string email)
        {
            return _dbSet.AsNoTracking().Any(u => u.Email.Equals(email));
        }

        public User? GetByEmail(string email)
        {
            return _dbSet.AsNoTracking()
                .FirstOrDefault(u => u.Email == email);
        }

        public User? GetByEmailTracked(string email)
        {
            return _dbSet.FirstOrDefault(u => u.Email == email);
        }

        public User? GetByResetToken(string token)
        {
            return _dbSet.FirstOrDefault(u =>
                u.PasswordResetToken == token &&
                u.PasswordResetTokenExpiry > DateTime.UtcNow);
        }

        public User? GetByConfirmToken(string token)
        {
            return _dbSet.FirstOrDefault(u => u.EmailConfirmationToken == token);
        }
    }
}
