using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface ITestDriveAppointmentRepository
    {
        IEnumerable<TestDriveAppointment> GetAll();
        TestDriveAppointment? GetById(int id);
        void Add(TestDriveAppointment appointment);
        void Update(TestDriveAppointment appointment);
        void Delete(TestDriveAppointment appointment);
    }

}