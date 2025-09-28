using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace CarManagement.DAL.Repositories
{
    public class UserRepo
    {
        private readonly CarDealerDbContext _context;

        public UserRepo(CarDealerDbContext context)
        {
            _context = context;
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            return _context.Users
                .Include(u => u.Roles) 
                .FirstOrDefault(u => u.UserName == username && u.PasswordHash == password);
        }
    }
}
