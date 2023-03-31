using DiplomaAPI.Models;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IDiagnosisICPC2Repository
    {
        public List<DiagnosisICPC2> GetAll();

        public List<DiagnosisICPC2> GetReasons();

        public DiagnosisICPC2 GetDiagnosis(int id);
    }
}
