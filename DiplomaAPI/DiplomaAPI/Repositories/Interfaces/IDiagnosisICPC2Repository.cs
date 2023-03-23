using DiplomaAPI.Models;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IDiagnosisICPC2Repository
    {
        public List<DiagnosisICPC2> GetAll();

        public DiagnosisICPC2 GetDiagnosis(int id);
    }
}
