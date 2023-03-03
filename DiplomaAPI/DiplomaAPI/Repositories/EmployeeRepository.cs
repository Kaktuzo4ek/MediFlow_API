using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.Employee;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using SendGrid.Helpers.Errors.Model;
using System.Web;

namespace DiplomaAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private DataContext _data;
        private IPasswordHasher _passwordHasher;

        public EmployeeRepository(DataContext data, IPasswordHasher passwordHasher)
        {
            _data = data;
            _passwordHasher = passwordHasher;
        }

        public Institution setInstitition(int id)
        {
           return _data.Institutions.Find(id);
        }

        public Department setDepartment(int id)
        {
            return _data.Departments.Find(id);
        }


        public Position setPosition(int id)
        {
            return _data.Positions.Find(id);
        }

        public int getEmployeeCount()
        {
           return _data.Employees.Count();
        }

        public List<Employee> getAll()
        {
            var employees = _data.Employees.ToList();
            employees.ForEach(e =>
            {
                _data.Entry(e).Reference("Institution").Load();
                _data.Entry(e).Reference("Position").Load();
                _data.Entry(e).Reference("Department").Load();
            });

            return employees;
        }

        public List<Employee> getEmployeeFromCertainDepartment(int depId)
        {
            var employees = _data.Employees.ToList();
            employees.ForEach(e =>
            {
                _data.Entry(e).Reference("Institution").Load();
                _data.Entry(e).Reference("Position").Load();
                _data.Entry(e).Reference("Department").Load();
            });

            employees = employees.Where(x => x.Department.DepartmentId == depId).ToList();
            return employees;
        }

        public Employee getById(int id)
        {
            var employee = _data.Employees.Find(id);
            _data.Entry(employee).Reference("Institution").Load();
            _data.Entry(employee).Reference("Position").Load();
            _data.Entry(employee).Reference("Department").Load();
            return employee;
        }

        public List<Employee> getByEmail(string email)
        {
            var employee = _data.Employees.Where(x => x.Email == email).ToList();
            employee.ForEach(e =>
            {
                _data.Entry(e).Reference("Institution").Load();
                _data.Entry(e).Reference("Position").Load();
                _data.Entry(e).Reference("Department").Load();
            });
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

        public ListEmployees FilterEmployees(string filter, string filterBy, int depId)
        {
            var employees = _data.Employees.Where(x => x.Department.DepartmentId == depId);

            foreach (var employee in employees)
            {
                _data.Entry(employee).Reference("Position").Load();
            }
            

            switch (filterBy)
            {
                case "surname":
                    employees = employees.Where(x => x.Surname == filter); 
                    break;
                case "name":
                    employees = employees.Where(x => x.Name == filter);
                    break;
                case "patronymic":
                    employees = employees.Where(x => x.Patronymic == filter);
                    break;
                case "position":
                    employees = employees.Where(x => x.Position.PositionName == filter);
                    break;
            }

            var employeeFilterViewModels = new List<FilterEmployeeViewModel>();

            foreach(Employee employee in employees)
            {
                employeeFilterViewModels.Add(PrepareResponseFilter(employee));
            }

            return new ListEmployees
            {
                Data = employeeFilterViewModels,
                TotalCount = employees.Count()
            };
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
                Surname = employee.Surname,
                Name = employee.Name,
                Patronymic = employee.Patronymic,
                DateOfBirth = employee.DateOfBirth,
                PhoneNumber = employee.PhoneNumber,
            };
        }

        private FilterEmployeeViewModel PrepareResponseFilter(Employee employee)
        {
            return new FilterEmployeeViewModel
            {
                Id = employee.Id,
                Surname = employee.Surname,
                Name = employee.Name,
                Patronymic = employee.Patronymic,
                Position = employee.Position,
            };
        }
    }
}
