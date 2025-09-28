using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarManagement.DAL.Repositories;
using BusinessObject.Models;
namespace CarManagement.BLL.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository ;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _repository.GetAll();
        }

        public Customer GetCustomerById(int id)
        {
            var customer = _repository.GetById(id);
            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }
            return customer;
        }

        public void AddCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            if (string.IsNullOrWhiteSpace(customer.FullName))
            {
                throw new ArgumentException("Full Name is required.");
            }
            _repository.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            if (string.IsNullOrWhiteSpace(customer.FullName))
            {
                throw new ArgumentException("Full Name is required.");
            }
            _repository.Update(customer);
        }

        public void DeleteCustomer(int id)
        {
            _repository.Delete(id);
        }
    }
}

