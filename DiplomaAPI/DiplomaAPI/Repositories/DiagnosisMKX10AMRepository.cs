using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;

namespace DiplomaAPI.Repositories
{
    public class DiagnosisMKX10AMRepository : IDiagnosisMKX10AMRepository
    {
        private DataContext _data;
        public DiagnosisMKX10AMRepository(DataContext data)
        {
            _data = data;
        }

        public List<DiagnosisMKX10AM> GetAll()
        {
            var diagnoses = _data.DiagnosesMKX10AM.ToList();

            diagnoses.ForEach(d =>
            {
                _data.Entry(d).Reference("Category").Load();
            });

            return diagnoses;
        }

        public DiagnosisMKX10AM GetDiagnosis(string id)
        {
            var diagnosis = _data.DiagnosesMKX10AM.Find(id);

            _data.Entry(diagnosis).Reference("Category").Load();

            return diagnosis;
        }
    }
}
