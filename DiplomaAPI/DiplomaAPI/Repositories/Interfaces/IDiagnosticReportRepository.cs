using DiplomaAPI.Models;
using DiplomaAPI.ViewModels.AmbulatoryEpisode.Appointment;
using DiplomaAPI.ViewModels;
using DiplomaAPI.ViewModels.AmbulatoryEpisode.DiagnosisReport;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IDiagnosticReportRepository
    {
        public List<DiagnosticReport> GetAll(int patientId);

        public DiagnosticReport GetById(int id);

        public DiagnosticReportViewModel Create(CreateDiagnosticReportViewModel model);

        public DiagnosticReportViewModel Update(UpdateDiagnosticReportViewModel model);

        public DiagnosticReportViewModel Delete(int reportId);
    }
}
