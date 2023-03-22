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

        public Role setRole(int id)
        {
            return _data.Roles.Find(id);
        }

        public int getDoctorsCount()
        {
           return _data.Doctors.Count();
        }

        public List<Doctor> getAll()
        {
            var Doctors = _data.Doctors.Where(x => x.IsConfirmed == true).ToList();
            Doctors.ForEach(e =>
            {
                _data.Entry(e).Reference("Institution").Load();
                _data.Entry(e).Reference("Position").Load();
                _data.Entry(e).Reference("Department").Load();
                _data.Entry(e).Reference("Role").Load();
            });

            return Doctors;
        }

        public Doctor LoadObjects(Doctor doctor)
        {
            _data.Entry(doctor).Reference("Institution").Load();
            _data.Entry(doctor).Reference("Position").Load();
            _data.Entry(doctor).Reference("Department").Load();
            _data.Entry(doctor).Reference("Role").Load();

            return doctor;
        }

        public Doctor ConfirmDoctor(int doctorId)
        {
            var doctor = _data.Doctors.Find(doctorId);
            doctor.IsConfirmed = true;
            _data.Update(doctor);
            _data.SaveChanges();
            return doctor;
        }

        public Doctor DeclineDoctor(int doctorId)
        {
            var doctor = _data.Doctors.Find(doctorId);
            _data.Remove(doctor);
            _data.SaveChanges();
            return doctor;
        }

        public List<Doctor> GetNotConfirmDoctors(int institutionId)
        {
            var doctors = _data.Doctors.Where(x => x.Institution.InstitutionId == institutionId && x.IsConfirmed == false).ToList();

            doctors.ForEach(d =>
            {
                _data.Entry(d).Reference("Institution").Load();
                _data.Entry(d).Reference("Position").Load();
                _data.Entry(d).Reference("Department").Load();
                _data.Entry(d).Reference("Role").Load();
                _data.Entry(d).Reference("Position").Load();
            });

            return doctors;
        }

        public List<Doctor> getDoctorsFromCertainDepartment(int depId)
        {
            var Doctors = _data.Doctors.Where(x => x.IsConfirmed == true && x.Role.RoleName != "Головний лікар").ToList();
            Doctors.ForEach(e =>
            {
                _data.Entry(e).Reference("Institution").Load();
                _data.Entry(e).Reference("Position").Load();
                _data.Entry(e).Reference("Department").Load();
                _data.Entry(e).Reference("Role").Load();
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
            _data.Entry(employee).Reference("Role").Load();
            return employee;
        }

        public List<Doctor> getByEmail(string email)
        {
            var employee = _data.Doctors.Where(x => x.Email == email && x.IsConfirmed == true).ToList();
            employee.ForEach(e =>
            {
                _data.Entry(e).Reference("Institution").Load();
                _data.Entry(e).Reference("Position").Load();
                _data.Entry(e).Reference("Department").Load();
                _data.Entry(e).Reference("Role").Load();
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
    }
}
