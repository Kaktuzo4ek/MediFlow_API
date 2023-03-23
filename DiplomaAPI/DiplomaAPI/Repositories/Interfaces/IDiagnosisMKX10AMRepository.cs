using DiplomaAPI.Models;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IDiagnosisMKX10AMRepository
    {
        public List<DiagnosisMKX10AM> GetAll();

        public DiagnosisMKX10AM GetDiagnosis(string id);
    }
}
