using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.Referral;
using DiplomaAPI.ViewModels.ReferralPackage;
using SendGrid.Helpers.Errors.Model;

namespace DiplomaAPI.Repositories
{
    public class ReferralRepository : IReferralRepository
    {
        private DataContext _data;
        public ReferralRepository(DataContext data)
        {
            _data = data;
        }

        public List<Referral> getAll()
        {
            var referrals = _data.Referrals.ToList();
            referrals.ForEach(r =>
            {
                _data.Entry(r).Reference("Service").Load();
                _data.Entry(r.Service).Reference("Category").Load();
            });
            return referrals;
        }

        public Referral getById(int id)
        {
            var referral = _data.Referrals.Find(id);
            _data.Entry(referral).Reference("Service").Load();
            _data.Entry(referral.Service).Reference("Category").Load();
            return referral;
        }

        public ReferralViewModel Update(UpdateReferralViewModel data)
        {
            var referral = _data.Referrals.Find(data.ReferralId);

            if (referral == null)
            {
                throw new NotFoundException();
            }

            if (data.Priority != "")
            {
                referral.Priority = data.Priority;
            }

            if (data.ServiceId != "")
            {
                referral.Service = _data.Services.Find(data.ServiceId);
            }

            _data.Update(referral);

            _data.SaveChanges();

            return PrepareResponse(referral);
        }

        public ReferralViewModel Delete(int referralId, string referralPackageId)
        {
            var referral = _data.Referrals.Find(referralId);

            if(referral == null)
            {
                throw new NotFoundException();
            }

            _data.Referrals.Remove(referral);
            _data.SaveChanges();

            if(_data.Referrals.Where(x => x.ReferralPackageId == referralPackageId).Count() == 0)
            {
                var referralPackage = _data.ReferralPackages.Find(referralPackageId);
                _data.ReferralPackages.Remove(referralPackage);
                _data.SaveChanges();
            }

            return PrepareResponseDelete(referral);
        }

        private ReferralViewModel PrepareResponse(Referral referral)
        {
            _data.Entry(referral.Service).Reference("Category").Load();
            return new ReferralViewModel
            {
               ReferralId = referral.ReferralId,
               ReferralPackageId = referral.ReferralPackageId,
               Status = referral.Status,
               Priority = referral.Priority,
               ProcessStatus = referral.ProcessStatus,
               Service = referral.Service,
            };
        }

        private ReferralViewModel PrepareResponseDelete(Referral referral)
        {
            return new ReferralViewModel
            {
                ReferralId = referral.ReferralId,
                ReferralPackageId = referral.ReferralPackageId,
                Status = referral.Status,
                Priority = referral.Priority,
                ProcessStatus = referral.ProcessStatus,
                Service = referral.Service,
            };
        }
    }
}
