using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.Employee;
using DiplomaAPI.ViewModels.Referral;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DiplomaAPI.Repositories
{
    public class ReferralRepository : IReferralRepository
    {
        private DataContext _data;
        public ReferralRepository(DataContext data)
        {
            _data = data;
        }

        public List<Referral> getAll(int patientId)
        {
            var referrals = _data.Referrals.Where(x => x.Patient.PatientId == patientId).ToList();
            referrals.ForEach(e =>
            {
                _data.Entry(e).Reference("Doctor").Load();
                _data.Entry(e).Reference("Service").Load();
                _data.Entry(e).Reference("Patient").Load();
                _data.Entry(e).Reference("Category").Load();
            });
            return referrals;
        }

        public Referral getById(string id)
        {
            var referal = _data.Referrals.Find(id);
            _data.Entry(referal).Reference("Doctor").Load();
            _data.Entry(referal).Reference("Service").Load();
            _data.Entry(referal).Reference("Patient").Load();
            _data.Entry(referal).Reference("Category").Load();
            return referal;
        }

        public ReferralViewModel Update(UpdateReferralViewModel data)
        {
            var referral = _data.Referrals.Find(data.ReferralId);

            if (referral == null)
            {
                throw new NotFoundException();
            }

            _data.Entry(referral).Reference("Doctor").Load();
            _data.Entry(referral).Reference("Service").Load();
            _data.Entry(referral).Reference("Patient").Load();
            _data.Entry(referral).Reference("Category").Load();

            if (data.CategoryId != 0)
            {
                referral.Category = _data.ReferralCategories.Find(data.CategoryId);
            }

            if (data.ServiceId != "")
            {
                referral.Service = _data.Services.Find(data.ServiceId);
            }

            if (data.Priority != "")
            {
                referral.Priority = data.Priority;
            }

            _data.Update(referral);

            _data.SaveChanges();

            return PrepareResponse(referral);
        }

        public ReferralViewModel Create(CreateReferralViewModel data)
        {
            if (_data.Referrals.Find(data.ReferralId) != null || data.ReferralId == "")
            {
                throw new NotFoundException();
            }

            var referral = new Referral
            {
                ReferralId = data.ReferralId,
                Doctor = _data.Employees.Find(data.DoctorId),
                Status = "Активне",
                ProcessStatus = "В роботі",
                Priority = data.Priority,
                Category = _data.ReferralCategories.Find(data.CategoryId),
                Service = _data.Services.Find(data.ServiceId),
                Patient = _data.Patients.Find(data.PatientId),
                Date = DateTime.Now,
                Validity = DateTime.Now.AddYears(1),
            };

            _data.Referrals.Add(referral);
            _data.SaveChanges();

            return PrepareResponse(referral);
        }

        public ReferralViewModel Delete(string referralId)
        {
            var referral = _data.Referrals.Find(referralId);

            if(referral == null)
            {
                throw new NotFoundException();
            }

            _data.Referrals.Remove(referral);
            _data.SaveChanges();

            return PrepareResponse(referral);

        }

        private ReferralViewModel PrepareResponse(Referral referral)
        {
            return new ReferralViewModel
            {
                ReferralId = referral.ReferralId,
                Doctor = referral.Doctor,
                Status = referral.Status,
                ProcessStatus = referral.ProcessStatus,
                Priority = referral.Priority,
                Category = referral.Category,
                Service = referral.Service,
                Patient = referral.Patient,
                Date = referral.Date,
                Validity = referral.Validity,
            };
        }
    }
}
