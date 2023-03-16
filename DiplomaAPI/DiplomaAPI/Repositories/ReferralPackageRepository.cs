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
                }
            });
            return referralPackages;
        }

        public ReferralPackage getById(string id)
        {
            var referralPackage = _data.ReferralPackages.Find(id);
            _data.Entry(referralPackage).Reference("Doctor").Load();
            _data.Entry(referralPackage).Reference("Patient").Load();
            _data.Entry(referralPackage).Collection("Referrals").Load();
            foreach (var service in referralPackage.Referrals)
            {
                _data.Entry(service).Reference("Service").Load();
                _data.Entry(service.Service).Reference("Category").Load();
            }
            return referralPackage;
        }


        public ReferralPackageViewModel Create(CreateReferralPackageViewModel data)
        {
            if ((_data.ReferralPackages.Find(data.ReferralPackageId) != null || data.ReferralPackageId == "") && data.Services[0].Length == 0  && data.Priority == "")
            {
                throw new NotFoundException();
            }

            var refCount = _data.Referrals.Count();

            for (int i = 0; i < data.Services.Length; i++)
            {
                var service = _data.Services.Find(data.Services[i].ToString());

                _data.Entry(service).Reference("Category").Load();

                _data.Referrals.Add(new Referral
                {
                    ReferralPackageId = data.ReferralPackageId,
                    Service = service,
                    Priority = data.Priority,
                    Status = "Активне",
                    ProcessStatus = "Не погашене"
                });
            }

            var referralPackage = new ReferralPackage
            {
                ReferralPackageId = data.ReferralPackageId,
                Doctor = _data.Doctors.Find(data.DoctorId),
                Patient = _data.Patients.Find(data.PatientId),
                Date = DateTime.Now,
                Validity = DateTime.Now.AddYears(1)
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
