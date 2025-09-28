using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarManagement.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CarDealerDbContext _context;

        public CustomerRepository(CarDealerDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            return _context.Customers.Find(id);
        }

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void Update(Customer customer)
        {
            var existing = _context.Customers.Find(customer.Id);
            if (existing != null)
            {
                existing.FullName = customer.FullName;
                existing.Phone = customer.Phone;
                existing.Email = customer.Email;
                // CreatedAt is preserved, not updated
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Customer not found.");
            }
        }

        public void Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Customer not found.");
            }
        }
    }
}