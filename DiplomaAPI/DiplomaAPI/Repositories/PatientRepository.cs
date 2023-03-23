using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using System.Drawing;

namespace DiplomaAPI.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private DataContext _data;
        public PatientRepository(DataContext data)
        {
            _data = data;
        }

        public List<Patient> GetPatients()
        {
            return _data.Patients.ToList();
        }

        public Patient GetPatient(int id)
        {
            return _data.Patients.Find(id);
        }

        public List<Patient> SearchPatients(string fullname)
        {
            var patients = _data.Patients.ToList();
            var patientsFound = new List<Patient>();

            patientsFound = patients.Where(p => ((p.Surname + p.Name + p.Patronymic).ToLower()).Contains(fullname.ToLower())).ToList();

            return patientsFound;
        }
    }
}
