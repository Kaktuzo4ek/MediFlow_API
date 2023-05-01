using DiplomaAPI.Models;
using DiplomaAPI.ViewModels;
using DiplomaAPI.ViewModels.AmbulatoryEpisode;
using DiplomaAPI.ViewModels.AmbulatoryEpisode.Appointment;
using DiplomaAPI.ViewModels.AmbulatoryEpisode.DiagnosisReport;
using DiplomaAPI.ViewModels.Procedure;
using DiplomaAPI.ViewModels.Referral;
using DiplomaAPI.ViewModels.ReferralPackage;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IAmbulatoryEpisodeRepository
    {
        public List<AmbulatoryEpisode> GetAll(int patientId);

        public List<AmbulatoryEpisode> GetEpisode(int id);

        public AmbulatoryEpisodeViewModel Create(CreateAmbulatoryEpisodeViewModel model);

        public AmbulatoryEpisodeViewModel Update(UpdateAmbulatoryEpisodeViewModel model);

        public AmbulatoryEpisodeViewModel UpdateDiagnosis(int episodeId, string diagnosisId);

        public AmbulatoryEpisodeViewModel CreateReferralPackage(int episodeId, CreateReferralPackageViewModel model);

        public AmbulatoryEpisodeViewModel CreateProcedure(int episodeId, CreateProcedureViewModel model);

        public AmbulatoryEpisodeViewModel CreateDiagnosticReport(int episodeId, CreateDiagnosticReportViewModel model);

        public AmbulatoryEpisodeViewModel UpdateDiagnosticReport(int episodeId, UpdateDiagnosticReportViewModel model);

        public AmbulatoryEpisodeViewModel DeleteDiagnosticReport(int episodeId, int reportId);

        public AmbulatoryEpisodeViewModel Delete(int id);

        Task<UserManagerResponse> CompeleteEpisode(int episodeId);

    }
}
