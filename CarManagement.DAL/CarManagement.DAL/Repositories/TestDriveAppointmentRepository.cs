using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Repositories;

namespace CarManagement.DAL.Repositories
{
    public class TestDriveAppointmentRepository : ITestDriveAppointmentRepository
    {
        private readonly CarDealerDbContext _context;

        public TestDriveAppointmentRepository(CarDealerDbContext context)
        {
            _context = context;
        }

        // Lấy tất cả
        public IEnumerable<TestDriveAppointment> GetAll()
        {
            return _context.TestDriveAppointments
                .Include(a => a.Customer)
                .Include(a => a.Car)
                .ToList();
        }

        // Lấy theo Id
        public TestDriveAppointment? GetById(int id)
        {
            return _context.TestDriveAppointments
                .Include(a => a.Customer)
                .Include(a => a.Car)
                .FirstOrDefault(a => a.Id == id);
        }

        // Thêm
        public void Add(TestDriveAppointment appointment)
        {
            _context.TestDriveAppointments.Add(appointment);
            _context.SaveChanges();
        }

        // Cập nhật
        public void Update(TestDriveAppointment appointment)
        {
            _context.TestDriveAppointments.Update(appointment);
            _context.SaveChanges();
        }

        // Xóa
        public void Delete(TestDriveAppointment appointment)
        {
            _context.TestDriveAppointments.Remove(appointment);
            _context.SaveChanges();
        }
    }
}