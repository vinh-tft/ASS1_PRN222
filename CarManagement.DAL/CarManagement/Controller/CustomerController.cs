using System;
using System.Collections.Generic;
using System.Windows;
using BusinessObject.Models;
using CarManagement.BLL.Services;
using CarManagement.DAL.Repositories;

namespace CarManagement
{
    public class CustomerController
    {
        private readonly CustomerService _customerService;
        private readonly CustomerView _view;

        public CustomerController(CustomerView view)
        {
            _view = view;
            var repository = new CustomerRepository(new CarDealerDbContext()); // Replace with dependency injection
            _customerService = new CustomerService(repository);
        }

        public void AddCustomer(Customer customer)
        {
            if (customer == null)
            {
                _view.ShowMessage("Invalid customer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _customerService.AddCustomer(customer);
                _view.LoadCustomersToGrid(_customerService.GetAllCustomers());
                _view.ShowMessage("Customer added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Error adding customer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public void ViewCustomers()
        {
            try
            {
                _view.LoadCustomersToGrid(_customerService.GetAllCustomers());
                _view.ShowMessage("Customer list refreshed.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Error loading customers: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void UpdateCustomer()
        {
            if (string.IsNullOrWhiteSpace(_view.GetIdText()))
            {
                _view.ShowMessage("Please select a customer to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var customer = new Customer
                {
                    Id = int.Parse(_view.GetIdText()),
                    FullName = _view.GetFullName(),
                    Phone = string.IsNullOrWhiteSpace(_view.GetPhone()) ? null : _view.GetPhone(),
                    Email = string.IsNullOrWhiteSpace(_view.GetEmail()) ? null : _view.GetEmail(),
                    CreatedAt = DateTime.ParseExact(_view.txtCreatedAt.Text, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture)
                };

                _customerService.UpdateCustomer(customer);
                _view.LoadCustomersToGrid(_customerService.GetAllCustomers());
                _view.ShowMessage("Customer updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Error updating customer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public void DeleteCustomer()
        {
            string idText = _view.GetIdText();
            if (string.IsNullOrEmpty(idText) || !int.TryParse(idText, out int id))
            {
                _view.ShowMessage("Please select a customer to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = _view.ShowConfirm("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _customerService.DeleteCustomer(id);
                    _view.LoadCustomersToGrid(_customerService.GetAllCustomers());
                    _view.ShowMessage("Customer deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    _view.ShowMessage($"Error deleting customer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void HandleSelectionChanged()
        {
            if (_view.dgCustomers.SelectedItem is Customer selectedCustomer)
            {
                _view.PopulateInputsFromSelection(selectedCustomer);
            }
            else
            {
                _view.ClearInputs();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(_view.GetFullName()))
            {
                _view.ShowMessage("Full Name is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(_view.GetEmail()) && !IsValidEmail(_view.GetEmail()))
            {
                _view.ShowMessage("Please enter a valid email address.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(_view.GetPhone()) && !IsValidPhone(_view.GetPhone()))
            {
                _view.ShowMessage("Please enter a valid phone number (e.g., 10-11 digits).", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhone(string phone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\+?\d{10,11}$");
        }
    }
}