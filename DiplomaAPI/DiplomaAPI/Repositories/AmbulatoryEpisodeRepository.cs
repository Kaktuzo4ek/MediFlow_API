using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.ReferralPackage;
using SendGrid.Helpers.Errors.Model;
using DiplomaAPI.ViewModels;
using DiplomaAPI.ViewModels.AmbulatoryEpisode;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using DiplomaAPI.ViewModels.AmbulatoryEpisode.Appointment;
using static System.Runtime.InteropServices.JavaScript.JSType;
using DiplomaAPI.ViewModels.Procedure;
using DiplomaAPI.ViewModels.AmbulatoryEpisode.DiagnosisReport;

namespace DiplomaAPI.Repositories
{
    public class AmbulatoryEpisodeRepository : IAmbulatoryEpisodeRepository
    {
        private DataContext _data;
        public AmbulatoryEpisodeRepository(DataContext data)
        {
            _data = data;
        }

        public List<AmbulatoryEpisode> GetAll(int patientId)
        {
            var episodes = _data.AmbulatoryEpisodes.Where(x => x.Patient.PatientId == patientId).ToList();

            episodes.ForEach(episode =>
            {
                _data.Entry(episode).Reference("Doctor").Load();
                _data.Entry(episode).Reference("Patient").Load();
                _data.Entry(episode).Collection("Appointments").Load();
                _data.Entry(episode).Reference("DiagnosisMKX10AM").Load();
                _data.Entry(episode).Reference("ReferralPackage").Load();
                _data.Entry(episode).Collection("Procedure").Load();
            });

            return episodes;
        }


        public List<AmbulatoryEpisode> GetEpisode(int id)
        {
            var episode = _data.AmbulatoryEpisodes.Where(x => x.EpisodeId == id).ToList();

            episode.ForEach(epis =>
            {
                _data.Entry(epis).Reference("Doctor").Load();
                _data.Entry(epis.Doctor).Reference("Institution").Load();
                _data.Entry(epis).Reference("Patient").Load();
                _data.Entry(epis).Collection("Appointments").Load();
                _data.Entry(epis).Collection("DiagnosticReports").Load();
                foreach (var entry in epis.DiagnosticReports)
                {
                    _data.Entry(entry).Reference("Service").Load();
                    if(entry.Service != null)
                        _data.Entry(entry.Service).Reference("Category").Load();
                    _data.Entry(entry).Reference("ExecutantDoctor").Load();
                    _data.Entry(entry).Reference("InterpretedDoctor").Load();
                }
                foreach (var entry in epis.Appointments)
                {
                    _data.Entry(entry).Collection("AppointmentsAndDiagnosesICPC2").Load();
                    foreach (var diagnoses in entry.AppointmentsAndDiagnosesICPC2)
                    {
                        _data.Entry(diagnoses).Reference("Appointment").Load();
                        _data.Entry(diagnoses).Reference("DiagnosisICPC2").Load();
                        //_data.Entry(diagnoses.DiagnosisICPC2.Category).Reference("Category").Load();
                    }

                    _data.Entry(entry).Collection("AppointmentsAndServices").Load();
                    foreach (var appAndServ in entry.AppointmentsAndServices)
                    {
                        _data.Entry(appAndServ).Reference("Appointment").Load();
                        _data.Entry(appAndServ).Reference("Service").Load();
                    }
                }
                _data.Entry(epis).Reference("DiagnosisMKX10AM").Load();
                _data.Entry(epis).Reference("ReferralPackage").Load();
                _data.Entry(epis).Collection("Procedure").Load();
            });

            return episode;
        }

        public AmbulatoryEpisodeViewModel Create(CreateAmbulatoryEpisodeViewModel model)
        {

            var episode = new AmbulatoryEpisode
            {
                Doctor = _data.Doctors.Find(model.DoctorId),
                Patient = _data.Patients.Find(model.PatientId),
                DiagnosisMKX10AM = _data.DiagnosesMKX10AM.Find(model.DiagnosisId),
                Status = "Діючий",
                Name = model.Name,
                Type = model.Type,
                DateCreated = DateTime.Now,
            };

            _data.AmbulatoryEpisodes.Add(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public static string GenerateReferralPackageNumber(int length)
        {
            var random = new Random();
            var result = "";

            for (int i = 0; i < length; i++)
            {
                if (i % 4 == 0 && i != 0)
                {
                    result += "-";
                }

                result += random.Next(0, 10).ToString();
            }

            return result;
        }

        public AmbulatoryEpisodeViewModel CreateReferralPackage(int episodeId, CreateReferralPackageViewModel model)
        {

            var episode = _data.AmbulatoryEpisodes.Find(episodeId);

            if (episode == null)
                throw new NotFoundException();

            var referralPackageId = GenerateReferralPackageNumber(16);

            while (_data.ReferralPackages.Find(referralPackageId) != null)
            {
                referralPackageId = GenerateReferralPackageNumber(16);
            }

            for (int i = 0; i < model.Services.Length; i++)
            {
                var service = _data.Services.Find(model.Services[i].ToString());

                _data.Entry(service).Reference("Category").Load();

                _data.Referrals.Add(new Referral
                {
                    ReferralPackageId = referralPackageId,
                    Service = service,
                    Priority = model.Priority,
                    Status = "Активне",
                    ProcessStatus = "Не погашене"
                });
            }

            episode.ReferralPackage = new ReferralPackage
            {
                ReferralPackageId = referralPackageId,
                Doctor = _data.Doctors.Find(model.DoctorId),
                Patient = _data.Patients.Find(model.PatientId),
                Date = DateTime.Now,
                Validity = DateTime.Now.AddYears(1),
                ProcessStatus = "Не погашений"
            };

            _data.AmbulatoryEpisodes.Update(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public AmbulatoryEpisodeViewModel CreateDiagnosticReport(int episodeId, CreateDiagnosticReportViewModel model)
        {
            var episode = _data.AmbulatoryEpisodes.Find(episodeId);

            if (episode == null)
                throw new NotFoundException();

            _data.Entry(episode).Collection("DiagnosticReports").Load();

            episode.DiagnosticReports.Add(new DiagnosticReport
            {
                Service = _data.Services.Find(model.ServiceId),
                Category = model.Category,
                Conclusion = model.Conclusion,
                Date = DateTime.Now,
                ExecutantDoctor = _data.Doctors.Find(model.ExecutantDoctorId),
                InterpretedDoctor = _data.Doctors.Find(model.InterpretedDoctorId)
            });

            _data.AmbulatoryEpisodes.Update(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public AmbulatoryEpisodeViewModel UpdateDiagnosticReport(int episodeId, UpdateDiagnosticReportViewModel model)
        {
            var episode = _data.AmbulatoryEpisodes.Find(episodeId);

            _data.Entry(episode).Collection("DiagnosticReports").Load();

            DiagnosticReport diagnosticReport = episode.DiagnosticReports.Where(x => x.ReportId == model.ReportId).ToList().ElementAt(0);


            if (episode == null)
                throw new NotFoundException();

            _data.Entry(diagnosticReport).Reference("Service").Load();
            _data.Entry(diagnosticReport).Reference("ExecutantDoctor").Load();
            _data.Entry(diagnosticReport).Reference("InterpretedDoctor").Load();


            if (model.ServiceId != diagnosticReport.Service.ServiceId)
            {
                episode.DiagnosticReports.First(x => x.ReportId == model.ReportId).Service = _data.Services.Find(model.ServiceId);
            }

            if (model.Category != diagnosticReport.Category)
            {
                episode.DiagnosticReports.First(x => x.ReportId == model.ReportId).Category = model.Category;
            }

            if (model.Conclusion != diagnosticReport.Conclusion)
            {
                episode.DiagnosticReports.First(x => x.ReportId == model.ReportId).Conclusion = model.Conclusion;
            }

            if (model.ExecutantDoctorId != diagnosticReport.ExecutantDoctor.Id)
            {
                episode.DiagnosticReports.First(x => x.ReportId == model.ReportId).ExecutantDoctor = _data.Doctors.Find(model.ExecutantDoctorId);
            }

            if (model.InterpretedDoctorId != diagnosticReport.InterpretedDoctor.Id)
            {
                episode.DiagnosticReports.First(x => x.ReportId == model.ReportId).InterpretedDoctor = _data.Doctors.Find(model.InterpretedDoctorId);
            }

            _data.AmbulatoryEpisodes.Update(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public AmbulatoryEpisodeViewModel DeleteDiagnosticReport(int episodeId, int reportId)
        {
            var episode = _data.AmbulatoryEpisodes.Find(episodeId);

            _data.Entry(episode).Collection("DiagnosticReports").Load();

            DiagnosticReport diagnosticReport = episode.DiagnosticReports.Where(x => x.ReportId == reportId).ToList().ElementAt(0);


            if (episode == null)
                throw new NotFoundException();

            _data.Entry(diagnosticReport).Reference("Service").Load();
            _data.Entry(diagnosticReport).Reference("ExecutantDoctor").Load();
            _data.Entry(diagnosticReport).Reference("InterpretedDoctor").Load();

            episode.DiagnosticReports.Remove(diagnosticReport);

            _data.AmbulatoryEpisodes.Update(episode);
            _data.DiagnosticReports.Remove(diagnosticReport);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public AmbulatoryEpisodeViewModel CreateProcedure(int episodeId, CreateProcedureViewModel model)
        {
            var episode = _data.AmbulatoryEpisodes.Find(episodeId);

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

            _data.Entry(episode).Collection("Procedure").Load();

            episode.Procedure.Add(new Procedure
            {
                Referral = _data.Referrals.Find(referralId),
                Doctor = _data.Doctors.Find(model.DoctorId),
                Patient = _data.Patients.Find(model.PatientId),
                Status = model.Status,
                EventDate = DateTime.Now,
                DateCreated = DateTime.Now,
                ProcedureName = '(' + service.ServiceId + ") " + service.ServiceName,
                Category = service.Category.CategoryName,
            });

            var referral = _data.Referrals.Find(referralId);

            if(referral != null)
            {
                referral.ProcessStatus = "Погашене " + "(від " + DateTime.Now + ")";
                _data.Referrals.Update(referral);
            }

            _data.AmbulatoryEpisodes.Update(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public AmbulatoryEpisodeViewModel Update(UpdateAmbulatoryEpisodeViewModel model)
        {
            var episode = _data.AmbulatoryEpisodes.Find(model.EpisodeId);

            if(episode == null)
                throw new NotFoundException();

            episode.DiagnosisMKX10AM = _data.DiagnosesMKX10AM.Find(model.DiagnosisId);

            if (model.Name != "")
                episode.Name = model.Name;

            if (model.Type != "")
                episode.Type = model.Type;


            _data.Update(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public AmbulatoryEpisodeViewModel UpdateDiagnosis(int episodeId, string diagnosisId)
        {
            var episode = _data.AmbulatoryEpisodes.Find(episodeId);

            if (episode == null)
                throw new NotFoundException();

            _data.Entry(episode).Reference("DiagnosisMKX10AM").Load();

            if(episode.DiagnosisMKX10AM != null)
            {
                if(episode.DiagnosisMKX10AM.DiagnosisId != diagnosisId)
                {
                    episode.DiagnosisMKX10AM = _data.DiagnosesMKX10AM.Find(diagnosisId);
                }
            } else
            {
                episode.DiagnosisMKX10AM = _data.DiagnosesMKX10AM.Find(diagnosisId);
            }

            _data.AmbulatoryEpisodes.Update(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public AmbulatoryEpisodeViewModel Delete(int id)
        {
            var episode = _data.AmbulatoryEpisodes.Find(id);

            if (episode == null)
                throw new NotFoundException();

            _data.AmbulatoryEpisodes.Remove(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        private AmbulatoryEpisodeViewModel PrepareResponse(AmbulatoryEpisode episode)
        {
            return new AmbulatoryEpisodeViewModel
            {
                EpisodeId = episode.EpisodeId,
                Doctor = episode.Doctor,
                Patient = episode.Patient,
                Status = episode.Status,
                Name = episode.Name,
                Type = episode.Type,
                DateCreated = episode.DateCreated
            };
        }
    }
}
