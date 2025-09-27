using BusinessObject.Models;
using CarManagement.DAL.Repositories.Interface;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CarManagement.DAL.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly MyDbContext _context;

        public CarRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Car>> GetAllAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car?> GetByIdAsync(int id)
        {
            return await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
