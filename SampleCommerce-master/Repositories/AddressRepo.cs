using Microsoft.EntityFrameworkCore;
using SampleCommerce.Context;
using SampleCommerce.Models;

namespace SampleCommerce.Repositories
{
    public class AddressRepo : BaseRepo<Address, int>, IRepoWrite<Address>, IRepoRead<int, Address>
    {
        public AddressRepo(EcommerceDbContext context) : base(context) { }

        public override Address? GetById(int id)
        {
            return _dbSet
                .Include(a => a.UserAddresses)
                .FirstOrDefault(a => a.Id == id);
        }
        public override List<Address> GetAll()
        {
            return _dbSet
                .AsNoTracking()
                .Include(a => a.UserAddresses)
                .ToList();
        }
        public UserAddress? GetUserAddress(int addressId, Guid userId)
        {
            return _context.UserAddresses
                .FirstOrDefault(ua => ua.AddressId == addressId && ua.UserId == userId);
        }
        public List<Address>? GetAllByUser(Guid userId)
        {
            return _context.UserAddresses
                .Where(ua => ua.UserId == userId)
                .Select(ua => ua.Address)
                .Include(a => a.UserAddresses)
                .ToList();
        }

        public void ClearPreferredAddress(Guid userId)
        {
            List<UserAddress>? preferredLinks = _context.UserAddresses
                .Where(ua => ua.UserId == userId && ua.IsPreferred)
                .ToList();

            foreach (var link in preferredLinks)
            {
                link.IsPreferred = false;
            }
            // qui faccio SaveChanges separato — da rifattorizzare con unit of work
            _context.SaveChanges();
        }

        public void DeleteUserAddress(UserAddress userLink)
        {
            _context.UserAddresses.Remove(userLink);
            _context.SaveChanges();
        }

        public bool CheckForOtherUsers(int id, Guid userId)
        {
            return _context.UserAddresses.Any(ua => ua.AddressId == id && ua.UserId != userId);
        }
    }
}
