using DiplomaAPI.Models;
using DiplomaAPI.ViewModels.Procedure;
using DiplomaAPI.ViewModels.ReferralPackage;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IProcedureRepository
    {
        public List<Procedure> getAll(int patientId);

        public Procedure getById(int id);

        public ProcedureViewModel Create(CreateProcedureViewModel data);

        public ProcedureViewModel Update(UpdateProcedureViewModel data);

        public ProcedureViewModel Delete(int procedureId);
    }
}
