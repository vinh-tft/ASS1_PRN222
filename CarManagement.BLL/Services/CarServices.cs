using BusinessObject.Models;
using CarManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagement.BLL.Services
{
    public class CarServices
    {
        private readonly CarRepo _repo;
        
        public CarServices(CarRepo repo)
        {
            _repo = repo;
        }

        public List<Car> GetAllCars() => _repo.GetAll();

        public Car? GetCar(int id) => _repo.GetById(id);

        public void CreateCar(Car car)
        {
            if (car.Price < 0)
                throw new System.Exception("Giá xe không hợp lệ");
            _repo.Add(car);
        }

        public void UpdateCar(Car car)
        {
            if (car.Price < 0)
                throw new System.Exception("Giá xe không hợp lệ");
            _repo.Update(car);
        }

        public void DeleteCar(int id) => _repo.Delete(id);
    }
}
