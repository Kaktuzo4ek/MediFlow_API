using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.Employee;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using SendGrid.Helpers.Errors.Model;

namespace DiplomaAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private DataContext _data;
        private IPasswordHasher _passwordHasher;
        private ILookupNormalizer _lookupNormalizer;
        public EmployeeRepository(DataContext data, IPasswordHasher passwordHasher, ILookupNormalizer lookupNormalizer)
        {
            _data = data;
            _passwordHasher = passwordHasher;
            _lookupNormalizer = lookupNormalizer;
        }

        public List<Employee> getAll()
        {
            var employees = _data.Employees.ToList();
            return employees;
        }

        public Employee getById(int id)
        {
            var employee = _data.Employees.Find(id);
            return employee;
        }

        public List<Employee> getByEmail(string email)
        {
            var employee = _data.Employees.Where(x => x.Email == email).ToList();
            return employee;
        }

        public EmployeeViewModel Update(UpdateEmployeeViewModel data)
        {
            var employee = _data.Employees.Find(data.Id);

            if (employee == null)
            {
                throw new NotFoundException();
            }

            if (data.Surname != null)
            {
                employee.Surname = data.Surname;
            }

            if (data.Name != null)
            {
                employee.Name = data.Name;
            }

            if (data.Patronymic != null)
            {
                employee.Patronymic = data.Patronymic;
            }

            if (data.DateOfBirth != null)
            {
                employee.DateOfBirth = (DateTime)data.DateOfBirth;
            }

            if (data.PhoneNumber != null)
            {
                employee.PhoneNumber = data.PhoneNumber;
            }

            if (data.Gender != null)
            {
                employee.Gender = data.Gender;
            }

            if(data.Password != "")
            { 
                employee.PasswordHash = _passwordHasher.HashPassword(data.Password);
            }

            _data.Update(employee);

            _data.SaveChanges();

            return PrepareResponse(employee);
        }

        private EmployeeViewModel PrepareResponse(Employee employee)
        {
            return new EmployeeViewModel
            {
                Id = employee.Id,
                Gender = employee.Gender,
                UserName = employee.UserName,
                NormalizedUserName = employee.NormalizedUserName,
                Email = employee.Email,
                NormalizedEmail = employee.NormalizedEmail,
                EmailConfirmed = employee.EmailConfirmed,
                PasswordHash = employee.PasswordHash,
                InstitutionId = employee.InstitutionId,
                DepartmentId = employee.DepartmentId,
                Surname = employee.Surname,
                Name = employee.Name,
                Patronymic = employee.Patronymic,
                DateOfBirth = employee.DateOfBirth,
                PhoneNumber = employee.PhoneNumber,
                PositionId = employee.PositionId,
            };
        }
    }
}
