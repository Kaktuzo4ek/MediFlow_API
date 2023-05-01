using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.AmbulatoryEpisode.DiagnosisReport;
using SendGrid.Helpers.Errors.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DiplomaAPI.Repositories
{
    public class DiagnosticReportRepisitory : IDiagnosticReportRepository
    {
        private DataContext _data;
        public DiagnosticReportRepisitory(DataContext data)
        {
            _data = data;
        }

        public List<DiagnosticReport> GetAll(int patientId)
        {
            var reports = _data.DiagnosticReports.ToList();
            reports.ForEach(i =>
            {
                _data.Entry(i).Reference("Patient").Load();
                _data.Entry(i).Reference("Service").Load();
                _data.Entry(i.Service).Reference("Category").Load();
                _data.Entry(i).Reference("Referral").Load();
                _data.Entry(i).Reference("ExecutantDoctor").Load();
                _data.Entry(i).Reference("InterpretedDoctor").Load();
            });
            reports = reports.Where(x => x.Patient.PatientId == patientId).ToList();
            return reports;
        }

        public DiagnosticReport GetById(int id)
        {
            var report = _data.DiagnosticReports.Find(id);
            _data.Entry(report).Reference("Patient").Load();
            _data.Entry(report).Reference("Service").Load();
            _data.Entry(report.Service).Reference("Category").Load();
            _data.Entry(report).Reference("Referral").Load();
            _data.Entry(report).Reference("ExecutantDoctor").Load();
            _data.Entry(report.ExecutantDoctor).Reference("Institution").Load();
            _data.Entry(report).Reference("InterpretedDoctor").Load();
            return report;
        }

        public DiagnosticReportViewModel Create(CreateDiagnosticReportViewModel model)
        {
            var referralTmp = _data.Referrals.Where(x => x.ReferralPackageId == model.ReferralPackageId && x.Service.ServiceId == model.ServiceId).ToList();
            var service = _data.Services.Find(model.ServiceId);

            _data.Entry(service).Reference("Category").Load();

            if (referralTmp == null)
            {
                throw new NotFoundException();
            }

            int referralId = 0;
            referralTmp.ForEach(r =>
            {
                referralId = r.ReferralId;
            });

            var report = new DiagnosticReport
            {
                Service = _data.Services.Find(model.ServiceId),
                Patient = _data.Patients.Find(model.PatientId),
                Referral = _data.Referrals.Find(referralId),
                Category = model.Category,
                Conclusion = model.Conclusion,
                Date = DateTime.Now,
                ExecutantDoctor = _data.Doctors.Find(model.ExecutantDoctorId),
                InterpretedDoctor = _data.Doctors.Find(model.InterpretedDoctorId)
            };

            var referral = _data.Referrals.Find(referralId);

            referral.ProcessStatus = "Погашене " + "(від " + DateTime.Now + ")";

            _data.Referrals.Update(referral);
            _data.DiagnosticReports.Add(report);
            _data.SaveChanges();

            return PrepareResponse(report);
        }

        public DiagnosticReportViewModel Update(UpdateDiagnosticReportViewModel model)
        {
            var report = _data.DiagnosticReports.Find(model.ReportId);

            if (report == null)
                throw new NotFoundException();

            _data.Entry(report).Reference("ExecutantDoctor").Load();
            _data.Entry(report).Reference("InterpretedDoctor").Load();


            if (model.Category != report.Category)
            {
                report.Category = model.Category;
            }

            if (model.Conclusion != report.Conclusion)
            {
                report.Conclusion = model.Conclusion;
            }

            if (model.ExecutantDoctorId != report.ExecutantDoctor.Id)
            {
                report.ExecutantDoctor = _data.Doctors.Find(model.ExecutantDoctorId);
            }

            if (model.InterpretedDoctorId != report.InterpretedDoctor.Id)
            {
                report.InterpretedDoctor = _data.Doctors.Find(model.InterpretedDoctorId);
            }

            _data.DiagnosticReports.Update(report);
            _data.SaveChanges();

            return PrepareResponse(report);
        }

        public DiagnosticReportViewModel Delete(int reportId)
        {
            var report = _data.DiagnosticReports.Find(reportId);

            if (report == null)
                throw new NotFoundException();

            if(report.Referral != null)
            {
                _data.Entry(report).Reference("Referral").Load();
                var referral = _data.Referrals.Find(report.Referral.ReferralId);
                referral.ProcessStatus = "Не погашене";
                _data.Referrals.Update(referral);
            }

            _data.DiagnosticReports.Remove(report);
            _data.SaveChanges();

            return PrepareResponse(report);
        }

        private DiagnosticReportViewModel PrepareResponse(DiagnosticReport report)
        {
            return new DiagnosticReportViewModel
            {
                ReportId = report.ReportId,
                Patient = report.Patient,
                Service = report.Service,
                Category = report.Category,
                Conclusion = report.Conclusion,
                Date = report.Date,
                ExecutantDoctor = report.ExecutantDoctor,
                InterpretedDoctor = report.InterpretedDoctor,
            };
        }
    }
}