using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiplomaAPI.Repositories
{
    public class DiagnosisICPC2Repository : IDiagnosisICPC2Repository
    {
        private DataContext _data;
        public DiagnosisICPC2Repository(DataContext data)
        {
            _data = data;
        }

        public List<DiagnosisICPC2> GetAll()
        {
            var diagnoses = _data.DiagnosesICPC2.ToList();

            diagnoses.ForEach(d =>
            {
                _data.Entry(d).Reference("Category").Load();
            });

            return diagnoses;
        }

        public List<DiagnosisICPC2> GetReasons()
        {
            var diagnoses = _data.DiagnosesICPC2.Where(x => x.Category.CategoryName == "Причини").ToList();

            diagnoses.ForEach(d =>
            {
                _data.Entry(d).Reference("Category").Load();
            });

            return diagnoses;
        }

        public DiagnosisICPC2 GetDiagnosis(int id)
        {
            var diagnosis = _data.DiagnosesICPC2.Find(id);

            _data.Entry(diagnosis).Reference("Category").Load();

            return diagnosis;
        }
    }
}
