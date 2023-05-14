using DiplomaAPI.Models;
using DiplomaAPI.ViewModels.AmbulatoryEpisode.DiagnosisReport;
using DiplomaAPI.ViewModels.AmbulatoryEpisode;
using DiplomaAPI.ViewModels.Procedure;
using DiplomaAPI.ViewModels.ReferralPackage;
using DiplomaAPI.ViewModels;
using DiplomaAPI.ViewModels.InpatientEpisode;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IInpatientEpisodeRepository
    {
        public List<InpatientEpisode> GetAll();

        public List<InpatientEpisode> GetAllForPatient(int patientId);

        public List<InpatientEpisode> GetEpisode(int id);

        public InpatientEpisodeViewModel Create(CreateInpatientEpisodeViewModel model);

        public InpatientEpisodeViewModel Update(UpdateInpatientEpisodeViewModel model);

        public InpatientEpisodeViewModel UpdateDiagnosis(int episodeId, string diagnosisId);

        public InpatientEpisodeViewModel CreateReferralPackage(int episodeId, CreateReferralPackageViewModel model);

        public InpatientEpisodeViewModel CreateProcedure(int episodeId, CreateProcedureViewModel model);

        public InpatientEpisodeViewModel CreateDiagnosticReport(int episodeId, CreateDiagnosticReportViewModel model);

        public InpatientEpisodeViewModel UpdateDiagnosticReport(int episodeId, UpdateDiagnosticReportViewModel model);

        public InpatientEpisodeViewModel DeleteDiagnosticReport(int episodeId, int reportId);

        public InpatientEpisodeViewModel Delete(int id);

        Task<UserManagerResponse> CompeleteEpisode(int episodeId);

        public InpatientEpisodeViewModel SetTreatingDoctor(int episodeId, int doctorId);

        public InpatientEpisodeViewModel SubmitPatient(int episodeId);

        public InpatientEpisodeViewModel DeclinePatient(int episodeId);

        public InpatientEpisodeViewModel DirectPatient(int episodeId, int departmentId);
    }
}
