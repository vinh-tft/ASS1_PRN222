using BusinessObject.Models;
using CarManagement.BLL.Services.Interface;
using CarManagement.DAL.Repositories.Interface;

namespace CarManagement.BLL.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _carRepository.GetAllAsync();
        }

        public async Task<Car?> GetCarByIdAsync(int id)
        {
            return await _carRepository.GetByIdAsync(id);
        }
    }
}
