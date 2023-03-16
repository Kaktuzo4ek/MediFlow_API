using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiplomaAPI.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private DataContext _data;
        public DepartmentRepository(DataContext data)
        {
            _data = data;
        }

        public List<Department> getAll()
        {
            var departments = _data.Departments.ToList();
            return departments;
        }

        public List<InstitutionAndDepartment> getInstitutionAndDepartment(int id)
        {
            var institutionAndDepartment = _data.InstitutionsAndDepartments.Where(x => x.InstitutionId == id).ToList();
            return institutionAndDepartment;
        }

        public Department getDepartment(int id)
        {
            var department = _data.Departments.Find(id);
            return department;
        }
    }
}
