using BusinessObject.Models;

namespace CarManagement.DAL.Repositories.Interface
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAllAsync();
        Task<Car?> GetByIdAsync(int id);
    }
}
