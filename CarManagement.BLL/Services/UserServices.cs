using BusinessObject.Models;
using CarManagement.DAL.Repositories;

namespace CarManagement.BLL.Services
{
    public class UserServices
    {
        private readonly UserRepo _userRepo;

        public UserServices(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public User Login(string username, string password)
        {
            return _userRepo.GetByUsernameAndPassword(username, password);
        }
    }
}
