using DiplomaAPI.Models;
using DiplomaAPI.ViewModels.Referral;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IReferralRepository
    {
        public List<Referral> getAll(int patientId);

        public Referral getById(string id);

        public ReferralViewModel Update(UpdateReferralViewModel data);

        public ReferralViewModel Create(CreateReferralViewModel data);

        public ReferralViewModel Delete(string referralId);
    }
}
