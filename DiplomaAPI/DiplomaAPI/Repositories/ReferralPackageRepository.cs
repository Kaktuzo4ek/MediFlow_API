using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.Employee;
using DiplomaAPI.ViewModels.ReferralPackage;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualBasic;
using SendGrid.Helpers.Errors.Model;
using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DiplomaAPI.Repositories
{
    public class ReferralPackageRepository : IReferralPackageRepository
    {
        private DataContext _data;
        public ReferralPackageRepository(DataContext data)
        {
            _data = data;
        }

        public List<ReferralPackage> getAll(int patientId)
        {
            var referralPackages = _data.ReferralPackages.Where(x => x.Patient.PatientId == patientId).ToList();
            referralPackages.ForEach(package =>
            {
                _data.Entry(package).Reference("Doctor").Load();
                _data.Entry(package).Reference("Patient").Load();
                _data.Entry(package).Collection("Referrals").Load();
                foreach(var service in package.Referrals)
                {
                    _data.Entry(service).Reference("Service").Load();
                    _data.Entry(service.Service).Reference("Category").Load();
                    _data.Entry(service).Reference("HospitalizationDepartment").Load();
                }
            });
            return referralPackages;
        }


        public List<ReferralPackage> getMyReferrals(int doctorId)
        {
            var referralPackages = _data.ReferralPackages.Where(x => x.Doctor.Id == doctorId).ToList();
            referralPackages.ForEach(package =>
            {
                _data.Entry(package).Reference("Doctor").Load();
                _data.Entry(package).Reference("Patient").Load();
                _data.Entry(package).Collection("Referrals").Load();
                foreach (var service in package.Referrals)
                {
                    _data.Entry(service).Reference("Service").Load();
                    _data.Entry(service.Service).Reference("Category").Load();
                    _data.Entry(service).Reference("HospitalizationDepartment").Load();
                }
            });
            return referralPackages;
        }

        public List<ReferralPackage> GetByEpisodeId(int episodeId)
        {
            var episode = _data.AmbulatoryEpisodes.Find(episodeId);

            if (episode == null)
                throw new NotFoundException();

            _data.Entry(episode).Reference("ReferralPackage").Load();

            var referralPackages = new List<ReferralPackage>();

            if (episode.ReferralPackage != null)
            {
                referralPackages = _data.ReferralPackages.Where(x => x.ReferralPackageId == episode.ReferralPackage.ReferralPackageId).ToList();

                referralPackages.ForEach(package =>
                {
                    _data.Entry(package).Reference("Doctor").Load();
                    _data.Entry(package).Reference("Patient").Load();
                    _data.Entry(package).Collection("Referrals").Load();
                    foreach (var service in package.Referrals)
                    {
                        _data.Entry(service).Reference("Service").Load();
                        _data.Entry(service.Service).Reference("Category").Load();
                        _data.Entry(service).Reference("HospitalizationDepartment").Load();
                    }
                });
            }
            return referralPackages;
        }

        public List<ReferralPackage> getById(string id)
        {
            var referralPackages = _data.ReferralPackages.Where(x => x.ReferralPackageId == id).ToList();
            referralPackages.ForEach(package =>
            {
                _data.Entry(package).Reference("Doctor").Load();
                _data.Entry(package.Doctor).Reference("Institution").Load();
                _data.Entry(package).Reference("Patient").Load();
                _data.Entry(package).Collection("Referrals").Load();
                foreach (var service in package.Referrals)
                {
                    _data.Entry(service).Reference("Service").Load();
                    _data.Entry(service.Service).Reference("Category").Load();
                    _data.Entry(service).Reference("HospitalizationDepartment").Load();
                }
            });
            return referralPackages;
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

        public ReferralPackageViewModel Create(CreateReferralPackageViewModel data)
        {

            var referralPackageId = GenerateReferralPackageNumber(16);

            while (_data.ReferralPackages.Find(referralPackageId) != null)
            {
                referralPackageId = GenerateReferralPackageNumber(16);
            }

            /*for (int i = 0; i < data.Services.Length; i++)
            {
                var service = _data.Services.Find(data.Services[i].ToString());

                _data.Entry(service).Reference("Category").Load();

                _data.Referrals.Add(new Referral
                {
                    ReferralPackageId = referralPackageId,
                    Service = service,
                    Priority = data.Priority,
                    Status = "Активне",
                    ProcessStatus = "Не погашене"
                });
            }*/

            var referralPackage = new ReferralPackage
            {
                ReferralPackageId = referralPackageId,
                Doctor = _data.Doctors.Find(data.DoctorId),
                Patient = _data.Patients.Find(data.PatientId),
                Date = DateTime.Now,
                Validity = DateTime.Now.AddYears(1),
                ProcessStatus = "Не погашений"
            };

            _data.ReferralPackages.Add(referralPackage);
            _data.SaveChanges();

            return PrepareResponse(referralPackage);
        }

        private ReferralPackageViewModel PrepareResponse(ReferralPackage referralPackage)
        {
            return new ReferralPackageViewModel
            {
                ReferralPackageId = referralPackage.ReferralPackageId,
                Doctor = referralPackage.Doctor,
                Patient = referralPackage.Patient,
                Date = referralPackage.Date,
                Validity = referralPackage.Validity,
                Referrals = referralPackage.Referrals
            };
        }
    }
}
