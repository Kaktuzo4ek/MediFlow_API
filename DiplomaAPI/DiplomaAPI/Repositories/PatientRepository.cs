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

        public List<Patient> SearchPatients(string surname, string name, string patronymic)
        {
            var patients = _data.Patients.ToList();
            var patientsFound = new List<Patient>();

            if(surname != "no")
                patientsFound = patients.Where(p => p.Surname == surname).ToList();
            else if(name != "no")
                patientsFound = patients.Where(p => p.Name == name).ToList();
            else if(patronymic != "no")
                patientsFound = patients.Where(p => p.Patronymic == patronymic).ToList();

            if(surname != "no" && name != "no")
                patientsFound = patients.Where(p => p.Surname == surname && p.Name == name).ToList();

            if(surname != "no" && name != "no" && patronymic != "no")
                patientsFound = patients.Where(p => p.Surname == surname && p.Name == name && p.Patronymic == patronymic).ToList();

            return patientsFound;
        }
    }
}
