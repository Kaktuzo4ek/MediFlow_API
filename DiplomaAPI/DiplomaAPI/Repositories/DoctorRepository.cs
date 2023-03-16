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
    public class DoctorRepository : IDoctorRepository
    {
        private DataContext _data;
        private IPasswordHasher _passwordHasher;

        public DoctorRepository(DataContext data, IPasswordHasher passwordHasher)
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

        public int getDoctorsCount()
        {
           return _data.Doctors.Count();
        }

        public List<Doctor> getAll()
        {
            var Doctors = _data.Doctors.ToList();
            Doctors.ForEach(e =>
            {
                _data.Entry(e).Reference("Institution").Load();
                _data.Entry(e).Reference("Position").Load();
                _data.Entry(e).Reference("Department").Load();
            });

            return Doctors;
        }

        public List<Doctor> getDoctorsFromCertainDepartment(int depId)
        {
            var Doctors = _data.Doctors.ToList();
            Doctors.ForEach(e =>
            {
                _data.Entry(e).Reference("Institution").Load();
                _data.Entry(e).Reference("Position").Load();
                _data.Entry(e).Reference("Department").Load();
            });

            Doctors = Doctors.Where(x => x.Department.DepartmentId == depId).ToList();
            return Doctors;
        }

        public Doctor getById(int id)
        {
            var employee = _data.Doctors.Find(id);
            _data.Entry(employee).Reference("Institution").Load();
            _data.Entry(employee).Reference("Position").Load();
            _data.Entry(employee).Reference("Department").Load();
            return employee;
        }

        public List<Doctor> getByEmail(string email)
        {
            var employee = _data.Doctors.Where(x => x.Email == email).ToList();
            employee.ForEach(e =>
            {
                _data.Entry(e).Reference("Institution").Load();
                _data.Entry(e).Reference("Position").Load();
                _data.Entry(e).Reference("Department").Load();
            });
            return employee;
        }

        public DoctorViewModel Update(UpdateDoctorViewModel data)
        {
            var employee = _data.Doctors.Find(data.Id);

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

        public ListDoctors FilterDoctors(string filter, string filterBy, int depId)
        {
            var Doctors = _data.Doctors.Where(x => x.Department.DepartmentId == depId);

            foreach (var employee in Doctors)
            {
                _data.Entry(employee).Reference("Position").Load();
            }
            

            switch (filterBy)
            {
                case "surname":
                    Doctors = Doctors.Where(x => x.Surname == filter); 
                    break;
                case "name":
                    Doctors = Doctors.Where(x => x.Name == filter);
                    break;
                case "patronymic":
                    Doctors = Doctors.Where(x => x.Patronymic == filter);
                    break;
                case "position":
                    Doctors = Doctors.Where(x => x.Position.PositionName == filter);
                    break;
            }

            var employeeFilterViewModels = new List<FilterDoctorViewModel>();

            foreach(Doctor employee in Doctors)
            {
                employeeFilterViewModels.Add(PrepareResponseFilter(employee));
            }

            return new ListDoctors
            {
                Data = employeeFilterViewModels,
                TotalCount = Doctors.Count()
            };
        }

        private DoctorViewModel PrepareResponse(Doctor employee)
        {
            return new DoctorViewModel
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

        private FilterDoctorViewModel PrepareResponseFilter(Doctor employee)
        {
            return new FilterDoctorViewModel
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
