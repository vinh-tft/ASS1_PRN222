using BusinessObject.Models;
using DataAccess.Repositories;
using System.Collections.Generic;

namespace CarManagement.BLL.Services
{
    public class TestDriveAppointmentService
    {
        private readonly ITestDriveAppointmentRepository _repository;

        public TestDriveAppointmentService(ITestDriveAppointmentRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TestDriveAppointment> GetAllAppointments()
        {
            return _repository.GetAll();
        }

        public TestDriveAppointment? GetAppointmentById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddAppointment(TestDriveAppointment appointment)
        {
            _repository.Add(appointment);
        }

        public void UpdateAppointment(TestDriveAppointment updated)
        {
            using (var context = new CarDealerDbContext())
            {
                var existing = context.TestDriveAppointments.Find(updated.Id);
                if (existing == null) return;

                // Cập nhật các field
                existing.CustomerId = updated.CustomerId;
                existing.CarId = updated.CarId;
                existing.AppointmentDate = updated.AppointmentDate;
                existing.Note = updated.Note;

                context.SaveChanges();
            }
        }


        public void DeleteAppointment(int id)
        {
            var appointment = _repository.GetById(id);
            if (appointment != null)
            {
                _repository.Delete(appointment);
            }
        }
    }
}