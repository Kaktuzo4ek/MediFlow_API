using DiplomaAPI.Models;
using DiplomaAPI.ViewModels.Referral;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IReferralRepository
    {
        public List<Referral> getAll();

        public Referral getById(int id);

        public ReferralViewModel Update(UpdateReferralViewModel data);

        public ReferralViewModel Delete(int referralId, string referralPackageId);
    }
}
