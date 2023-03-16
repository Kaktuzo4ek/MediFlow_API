using DiplomaAPI.Models;
using DiplomaAPI.ViewModels.ReferralPackage;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IReferralPackageRepository
    {
        public List<ReferralPackage> getAll(int patientId);

        public ReferralPackage getById(string id);

        public ReferralPackageViewModel Create(CreateReferralPackageViewModel data);
    }
}
