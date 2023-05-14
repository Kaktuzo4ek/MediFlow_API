using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.AmbulatoryEpisode.DiagnosisReport;
using DiplomaAPI.ViewModels.AmbulatoryEpisode;
using DiplomaAPI.ViewModels.Procedure;
using DiplomaAPI.ViewModels.ReferralPackage;
using DiplomaAPI.ViewModels;
using SendGrid.Helpers.Errors.Model;
using System.Collections.ObjectModel;
using DiplomaAPI.ViewModels.InpatientEpisode;
using Microsoft.AspNet.Identity;
using System.Numerics;

namespace DiplomaAPI.Repositories
{
    public class InpatientEpisodeRepository : IInpatientEpisodeRepository
    {
        private DataContext _data;
        public InpatientEpisodeRepository(DataContext data)
        {
            _data = data;
        }

        public List<InpatientEpisode> GetAll()
        {
            var episodes = _data.InpatientEpisodes.Where(x => x.PatientStatus != "Відхилено").ToList();

            episodes.ForEach(epis =>
            {
                _data.Entry(epis).Reference("HospitalizationDoctor").Load();
                _data.Entry(epis.HospitalizationDoctor).Reference("Position").Load();
                _data.Entry(epis).Reference("TreatingDoctor").Load();
                if(epis.TreatingDoctor != null)
                    _data.Entry(epis.TreatingDoctor).Reference("Position").Load();
                _data.Entry(epis).Reference("Patient").Load();
                _data.Entry(epis).Reference("MedicalCard").Load();
                _data.Entry(epis).Reference("Institution").Load();
                _data.Entry(epis).Reference("Department").Load();
                _data.Entry(epis).Reference("ReferralPackage").Load();
                if (epis.ReferralPackage != null)
                    _data.Entry(epis.ReferralPackage).Collection("Referrals").Load();
                _data.Entry(epis.HospitalizationDoctor).Reference("Institution").Load();
                _data.Entry(epis).Collection("Appointments").Load();
                _data.Entry(epis).Collection("DiagnosticReports").Load();
                foreach (var entry in epis.DiagnosticReports)
                {
                    _data.Entry(entry).Reference("Service").Load();
                    if (entry.Service != null)
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
                if (epis.ReferralPackage != null)
                    _data.Entry(epis.ReferralPackage).Collection("Referrals").Load();
                _data.Entry(epis).Collection("Procedure").Load();
            });

            return episodes;
        }

        public List<InpatientEpisode> GetAllForPatient(int patientId)
        {
            var episodes = _data.InpatientEpisodes.Where(x => x.Patient.PatientId == patientId).ToList();

            episodes.ForEach(epis =>
            {
                _data.Entry(epis).Reference("HospitalizationDoctor").Load();
                _data.Entry(epis.HospitalizationDoctor).Reference("Position").Load();
                _data.Entry(epis).Reference("TreatingDoctor").Load();
                if (epis.TreatingDoctor != null)
                    _data.Entry(epis.TreatingDoctor).Reference("Position").Load();
                _data.Entry(epis).Reference("Patient").Load();
                _data.Entry(epis).Reference("MedicalCard").Load();
                _data.Entry(epis).Reference("Institution").Load();
                _data.Entry(epis).Reference("Department").Load();
                _data.Entry(epis).Reference("ReferralPackage").Load();
                if (epis.ReferralPackage != null)
                    _data.Entry(epis.ReferralPackage).Collection("Referrals").Load();
                _data.Entry(epis.HospitalizationDoctor).Reference("Institution").Load();
                _data.Entry(epis).Collection("Appointments").Load();
                _data.Entry(epis).Collection("DiagnosticReports").Load();
                foreach (var entry in epis.DiagnosticReports)
                {
                    _data.Entry(entry).Reference("Service").Load();
                    if (entry.Service != null)
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
                if (epis.ReferralPackage != null)
                    _data.Entry(epis.ReferralPackage).Collection("Referrals").Load();
                _data.Entry(epis).Collection("Procedure").Load();
            });

            return episodes;
        }


        public List<InpatientEpisode> GetEpisode(int id)
        {
            var episode = _data.InpatientEpisodes.Where(x => x.EpisodeId == id).ToList();

            episode.ForEach(epis =>
            {
                _data.Entry(epis).Reference("HospitalizationDoctor").Load();
                _data.Entry(epis.HospitalizationDoctor).Reference("Position").Load();
                _data.Entry(epis).Reference("TreatingDoctor").Load();
                if (epis.TreatingDoctor != null)
                    _data.Entry(epis.TreatingDoctor).Reference("Position").Load();
                _data.Entry(epis).Reference("Patient").Load();
                _data.Entry(epis).Reference("MedicalCard").Load();
                _data.Entry(epis).Reference("Institution").Load();
                _data.Entry(epis).Reference("Department").Load();
                _data.Entry(epis).Reference("ReferralPackage").Load();
                if (epis.ReferralPackage != null)
                    _data.Entry(epis.ReferralPackage).Collection("Referrals").Load();
                _data.Entry(epis.HospitalizationDoctor).Reference("Institution").Load();
                _data.Entry(epis).Collection("Appointments").Load();
                _data.Entry(epis).Collection("DiagnosticReports").Load();
                foreach (var entry in epis.DiagnosticReports)
                {
                    _data.Entry(entry).Reference("Service").Load();
                    if (entry.Service != null)
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
                if (epis.ReferralPackage != null)
                    _data.Entry(epis.ReferralPackage).Collection("Referrals").Load();
                _data.Entry(epis).Collection("Procedure").Load();
            });

            return episode;
        }

        public InpatientEpisodeViewModel Create(CreateInpatientEpisodeViewModel model)
        {

            var doctor = _data.Doctors.Find(model.DoctorId);

            _data.Entry(doctor).Reference("Department").Load();
            _data.Entry(doctor).Reference("Institution").Load();

            var depName1 = (doctor.Department.Name.Replace("не", "ним") + 'м').ToLower();

            var referralPackage = _data.ReferralPackages.Find(model.ReferralPackageId);

            var temp = _data.Referrals.Where(x => x.ReferralPackageId == model.ReferralPackageId && x.Category == "Госпіталізація").ToList();

            var depName2 = doctor.Department.Name.ToLower();

            Referral referral = null;
            if (temp.Count != 0)
            {
                referral = temp.ElementAt(0);
                _data.Entry(referral).Reference("HospitalizationDepartment").Load();
                depName2 = referral.HospitalizationDepartment.Name.ToLower();
            }

            var episode = new InpatientEpisode
            {
                ReceiptDate = DateTime.Parse(model.ReceiptDate),
                HospitalizationDoctor = doctor,
                Patient = _data.Patients.Find(model.PatientId),
                PatientStatus = "Госпіталізовано " + depName1 + " у " + depName2 + ", але ще не прийнято",
                MedicalCard = new MedicalCard 
                {
                    DirectedFrom = model.DirectedFrom,
                    InstitutionName = model.InstitutionName,
                    InstitutionCode = model.InstitutionCode,
                    Referral = referral,
                    Patient = _data.Patients.Find(model.PatientId),
                    CountryAndCode = model.CountryAndCode,
                    DocumentType = model.DocumentType,
                    DocumentNumber = model.DocumentNumber,
                    IsWorking = model.IsWorking,
                    WorkPlace = model.WorkPlace
                },
                Institution = doctor.Institution,
                Department = referral != null ? referral.HospitalizationDepartment : doctor.Department,
                BedType = model.BedType,
                BenefitCategory = model.BenefitCategory,
                Status = "Діючий",
                Type = model.Type,
                DateCreated = DateTime.Now,
            };

            var patient = _data.Patients.Find(model.PatientId);

            if (patient == null)
                throw new NotFoundException();

            if(patient.Surname != model.PatientInfo.Surname && model.PatientInfo.Surname != "")
                patient.Surname = model.PatientInfo.Surname;

            if (patient.Name != model.PatientInfo.Name && model.PatientInfo.Name != "")
                patient.Name = model.PatientInfo.Name;

            if (patient.Patronymic != model.PatientInfo.Patronymic && model.PatientInfo.Patronymic != "")
                patient.Patronymic = model.PatientInfo.Patronymic;

            if (patient.PhoneNumber != model.PatientInfo.PhoneNumber && model.PatientInfo.PhoneNumber != "")
                patient.PhoneNumber = model.PatientInfo.PhoneNumber;

            if (patient.DateOfBirth != model.PatientInfo.DateOfBirth)
                patient.DateOfBirth = model.PatientInfo.DateOfBirth;

            if (patient.Gender != model.PatientInfo.Gender && model.PatientInfo.Gender != "")
                patient.Gender = model.PatientInfo.Gender;

            if (patient.Height != model.PatientInfo.Height && model.PatientInfo.Height != 0)
                patient.Height = model.PatientInfo.Height;

            if (patient.Weight != model.PatientInfo.Weight && model.PatientInfo.Weight != 0)
                patient.Weight = model.PatientInfo.Weight;

            if (patient.City != model.PatientInfo.City && model.PatientInfo.City != "")
                patient.City = model.PatientInfo.City;

            if (patient.Region != model.PatientInfo.Region && model.PatientInfo.Region != "")
                patient.Region = model.PatientInfo.Region;

            if (patient.District != model.PatientInfo.District && model.PatientInfo.District != "")
                patient.District = model.PatientInfo.District;

            if (patient.Street != model.PatientInfo.Street && model.PatientInfo.Street != "")
                patient.Street = model.PatientInfo.Street;

            if (patient.PostIndex != model.PatientInfo.PostIndex && model.PatientInfo.PostIndex != "")
                patient.PostIndex = model.PatientInfo.PostIndex;

            if (patient.BuildingNumber != model.PatientInfo.BuildingNumber && model.PatientInfo.BuildingNumber != "")
                patient.BuildingNumber = model.PatientInfo.BuildingNumber;

            if (referral != null)
            {
                referral.ProcessStatus = "Погашене " + "(від " + DateTime.Now + ")";
                _data.Referrals.Update(referral);
            }

            _data.InpatientEpisodes.Add(episode);
            _data.Patients.Update(patient);
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

        public InpatientEpisodeViewModel CreateReferralPackage(int episodeId, CreateReferralPackageViewModel model)
        {

            var episode = _data.InpatientEpisodes.Find(episodeId);

            if (episode == null)
                throw new NotFoundException();

            _data.Entry(episode).Reference("ReferralPackage").Load();
            if (episode.ReferralPackage != null)
                _data.Entry(episode.ReferralPackage).Collection("Referrals").Load();

            if (episode.ReferralPackage == null)
            {
                var referralPackageId = GenerateReferralPackageNumber(16);

                while (_data.ReferralPackages.Find(referralPackageId) != null)
                {
                    referralPackageId = GenerateReferralPackageNumber(16);
                }

                Collection<Referral> referrals = new Collection<Referral>();

                referrals.Add(new Referral
                {
                    ReferralPackageId = referralPackageId,
                    Doctor = _data.Doctors.Find(model.DoctorId),
                    Patient = _data.Patients.Find(model.PatientId),
                    Date = DateTime.Now,
                    Validity = DateTime.Now.AddYears(1),
                    Service = _data.Services.Find(model.ServiceId),
                    Priority = model.Priority,
                    Status = "Активне",
                    ProcessStatus = "Не погашене",
                    Category = model.Category,
                    HospitalizationDepartment = _data.Departments.Find(model.HospitalizationDepartmentId)
                });

                episode.ReferralPackage = new ReferralPackage
                {
                    ReferralPackageId = referralPackageId,
                    ProcessStatus = "Не погашений",
                    Doctor = _data.Doctors.Find(model.DoctorId),
                    Patient = _data.Patients.Find(model.PatientId),
                    Date = DateTime.Now,
                    Validity = DateTime.Now.AddYears(1),
                    Referrals = referrals
                };
            }
            else
            {
                episode.ReferralPackage.Referrals.Add(new Referral
                {
                    ReferralPackageId = episode.ReferralPackage.ReferralPackageId,
                    Doctor = _data.Doctors.Find(model.DoctorId),
                    Patient = _data.Patients.Find(model.PatientId),
                    Date = DateTime.Now,
                    Validity = DateTime.Now.AddYears(1),
                    Service = _data.Services.Find(model.ServiceId),
                    Priority = model.Priority,
                    Status = "Активне",
                    ProcessStatus = "Не погашене",
                    Category = model.Category,
                    HospitalizationDepartment = _data.Departments.Find(model.HospitalizationDepartmentId)
                });
            }

            _data.InpatientEpisodes.Update(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public InpatientEpisodeViewModel CreateDiagnosticReport(int episodeId, CreateDiagnosticReportViewModel model)
        {
            var episode = _data.InpatientEpisodes.Find(episodeId);

            if (episode == null)
                throw new NotFoundException();

            _data.Entry(episode).Collection("DiagnosticReports").Load();

            episode.DiagnosticReports.Add(new DiagnosticReport
            {
                Service = _data.Services.Find(model.ServiceId),
                Patient = _data.Patients.Find(model.PatientId),
                Category = model.Category,
                Conclusion = model.Conclusion,
                Date = DateTime.Now,
                ExecutantDoctor = _data.Doctors.Find(model.ExecutantDoctorId),
                InterpretedDoctor = _data.Doctors.Find(model.InterpretedDoctorId)
            });

            _data.InpatientEpisodes.Update(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public InpatientEpisodeViewModel UpdateDiagnosticReport(int episodeId, UpdateDiagnosticReportViewModel model)
        {
            var episode = _data.InpatientEpisodes.Find(episodeId);

            _data.Entry(episode).Collection("DiagnosticReports").Load();

            DiagnosticReport diagnosticReport = episode.DiagnosticReports.Where(x => x.ReportId == model.ReportId).ToList().ElementAt(0);


            if (episode == null)
                throw new NotFoundException();

            _data.Entry(diagnosticReport).Reference("ExecutantDoctor").Load();
            _data.Entry(diagnosticReport).Reference("InterpretedDoctor").Load();


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

            _data.InpatientEpisodes.Update(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public InpatientEpisodeViewModel DeleteDiagnosticReport(int episodeId, int reportId)
        {
            var episode = _data.InpatientEpisodes.Find(episodeId);

            _data.Entry(episode).Collection("DiagnosticReports").Load();

            DiagnosticReport diagnosticReport = episode.DiagnosticReports.Where(x => x.ReportId == reportId).ToList().ElementAt(0);


            if (episode == null)
                throw new NotFoundException();

            _data.Entry(diagnosticReport).Reference("Service").Load();
            _data.Entry(diagnosticReport).Reference("ExecutantDoctor").Load();
            _data.Entry(diagnosticReport).Reference("InterpretedDoctor").Load();

            episode.DiagnosticReports.Remove(diagnosticReport);

            _data.InpatientEpisodes.Update(episode);
            _data.DiagnosticReports.Remove(diagnosticReport);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public InpatientEpisodeViewModel CreateProcedure(int episodeId, CreateProcedureViewModel model)
        {
            var episode = _data.InpatientEpisodes.Find(episodeId);

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
                Notes = model.Notes,
            });

            var referral = _data.Referrals.Find(referralId);

            if (referral != null)
            {
                referral.ProcessStatus = "Погашене " + "(від " + DateTime.Now + ")";
                _data.Referrals.Update(referral);
            }

            _data.InpatientEpisodes.Update(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public InpatientEpisodeViewModel Update(UpdateInpatientEpisodeViewModel model)
        {
            var episode = _data.InpatientEpisodes.Find(model.EpisodeId);

            if (episode == null)
                throw new NotFoundException();

            episode.DiagnosisMKX10AM = _data.DiagnosesMKX10AM.Find(model.DiagnosisId);

            if (model.Name != "")
                episode.Name = model.Name;

            _data.Update(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public InpatientEpisodeViewModel UpdateDiagnosis(int episodeId, string diagnosisId)
        {
            var episode = _data.InpatientEpisodes.Find(episodeId);

            if (episode == null)
                throw new NotFoundException();

            _data.Entry(episode).Reference("DiagnosisMKX10AM").Load();

            if (episode.DiagnosisMKX10AM != null)
            {
                if (episode.DiagnosisMKX10AM.DiagnosisId != diagnosisId)
                {
                    episode.DiagnosisMKX10AM = _data.DiagnosesMKX10AM.Find(diagnosisId);
                }
            }
            else
            {
                episode.DiagnosisMKX10AM = _data.DiagnosesMKX10AM.Find(diagnosisId);
            }

            episode.Name = '(' + episode.DiagnosisMKX10AM?.DiagnosisId + ')' + " " + episode.DiagnosisMKX10AM?.DiagnosisName;

            _data.InpatientEpisodes.Update(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public InpatientEpisodeViewModel Delete(int id)
        {
            var episode = _data.InpatientEpisodes.Find(id);

            if (episode == null)
                throw new NotFoundException();

            _data.InpatientEpisodes.Remove(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public async Task<UserManagerResponse> CompeleteEpisode(int episodeId)
        {
            var episode = _data.InpatientEpisodes.Find(episodeId);

            if (episode == null)
                throw new NotFoundException();


            var referrals = new List<Referral>();

            _data.Entry(episode).Reference("ReferralPackage").Load();
            if (episode.ReferralPackage != null)
            {
                _data.Entry(episode.ReferralPackage).Collection("Referrals").Load();
                referrals = episode.ReferralPackage.Referrals.Where(x => x.ProcessStatus == "Не погашене").ToList();
            }

            if (referrals.Count() == 0)
            {
                episode.Status = "Завершений";
                _data.InpatientEpisodes.Update(episode);
                _data.SaveChanges();
            }
            else
            {
                return new UserManagerResponse
                {
                    Message = "not completed referrals",
                    IsSuccess = false
                };
            }

            return new UserManagerResponse
            {
                Message = "Episode successfully completed",
                IsSuccess = true
            };
        }

        public InpatientEpisodeViewModel SetTreatingDoctor(int episodeId, int doctorId)
        {
            var episode = _data.InpatientEpisodes.Find(episodeId);

            if (episode == null)
                throw new NotFoundException();

            episode.TreatingDoctor = _data.Doctors.Find(doctorId);
            _data.InpatientEpisodes.Update(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public InpatientEpisodeViewModel SubmitPatient(int episodeId)
        {
            var episode = _data.InpatientEpisodes.Find(episodeId);

            if (episode == null)
                throw new NotFoundException();

            episode.PatientStatus = "Прийнято";
            _data.InpatientEpisodes.Update(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public InpatientEpisodeViewModel DeclinePatient(int episodeId)
        {
            var episode = _data.InpatientEpisodes.Find(episodeId);

            if (episode == null)
                throw new NotFoundException();

            episode.PatientStatus = "Відхилено";
            _data.InpatientEpisodes.Update(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public InpatientEpisodeViewModel DirectPatient(int episodeId, int departmentId)
        {
            var episode = _data.InpatientEpisodes.Find(episodeId);

            if (episode == null)
                throw new NotFoundException();

            _data.Entry(episode).Reference("Department").Load();

            var department = _data.Departments.Find(departmentId);

            var depName1 = episode.Department.Name.Replace("не", "ного").ToLower();

            var depName2 = department?.Name.ToLower();

            episode.PatientStatus = "Переведено з " + depName1 + " у " + depName2 + ", але ще не прийнято";
            episode.Department = department;
            _data.InpatientEpisodes.Update(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        private InpatientEpisodeViewModel PrepareResponse(InpatientEpisode episode)
        {
            return new InpatientEpisodeViewModel
            {
                EpisodeId = episode.EpisodeId,
                ReceiptDate = episode.ReceiptDate,
                Doctor = episode.HospitalizationDoctor,
                Patient = episode.Patient,
                PatientStatus = episode.PatientStatus,
                MedicalCard = episode.MedicalCard,
                Institution = episode.Institution,
                Department = episode.Department,
                BedType = episode.BedType,
                BenefitCategory = episode.BenefitCategory,
                Appointments = episode.Appointments,
                DiagnosisMKX10AM = episode.DiagnosisMKX10AM,
                ReferralPackage = episode.ReferralPackage,
                Procedure = episode.Procedure,
                DiagnosticReports = episode.DiagnosticReports,
                Status = episode.Status,
                Name = episode.Name,
                Type = episode.Type,
                DateCreated = episode.DateCreated
            };
        }
    }
}
