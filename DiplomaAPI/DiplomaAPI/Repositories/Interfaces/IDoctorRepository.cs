using DiplomaAPI.Models;
using DiplomaAPI.ViewModels.Employee;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        public Institution setInstitition(int id);

        public Department setDepartment(int id);

        public Position setPosition(int id);

        public int getDoctorsCount();

        public List<Doctor> getAll();

        public List<Doctor> getDoctorsFromCertainDepartment(int depId);

        public Doctor getById(int id);

        public List<Doctor> getByEmail(string email);

        public DoctorViewModel Update(UpdateDoctorViewModel data);

        public ListDoctors FilterDoctors(string filter, string filterBy, int depId);
    }
}
