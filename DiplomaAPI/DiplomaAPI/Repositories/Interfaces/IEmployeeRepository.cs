using DiplomaAPI.Models;
using DiplomaAPI.ViewModels.Employee;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        public List<Employee> getAll();

        public Employee getById(int id);

        public List<Employee> getByEmail(string email);

        public EmployeeViewModel Update(UpdateEmployeeViewModel data);
    }
}
