using DiplomaAPI.Models;
using DiplomaAPI.ViewModels.Employee;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        public Institution setInstitition(int id);

        public Department setDepartment(int id);

        public Position setPosition(int id);

        public Role setRole(int id);

        public Doctor LoadObjects(Doctor doctor);

        public Doctor ConfirmDoctor(int doctorId);

        public Doctor DeclineDoctor(int doctorId);

        public List<Doctor> GetNotConfirmDoctors(int institutionId);

        public List<Doctor> GetDoctorsFromInstitution(int institutionId);

        public List<Doctor> GetDoctorsExcludeInstitution(int institutionId);

        public int getDoctorsCount();

        public List<Doctor> getAll();

        public List<Doctor> getDoctorsFromCertainDepartment(int depId);

        public Doctor getById(int id);

        public List<Doctor> getByEmail(string email);

        public DoctorViewModel Update(UpdateDoctorViewModel data);
    }
}
