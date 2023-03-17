using DiplomaAPI.Models;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        public List<Patient> GetPatients();

        public List<Patient> SearchPatients(string fullname);
    }
}
