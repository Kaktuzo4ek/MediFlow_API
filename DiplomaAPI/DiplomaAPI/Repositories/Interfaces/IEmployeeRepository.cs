using DiplomaAPI.Models;
using DiplomaAPI.ViewModels.Employee;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        public Institution setInstitition(int id);

        public Department setDepartment(int id);

        public Position setPosition(int id);

        public int getEmployeeCount();

        public List<Employee> getAll();

        public List<Employee> getEmployeeFromCertainDepartment(int depId);

        public Employee getById(int id);

        public List<Employee> getByEmail(string email);

        public EmployeeViewModel Update(UpdateEmployeeViewModel data);

        public ListEmployees FilterEmployees(string filter, string filterBy, int depId);
    }
}
