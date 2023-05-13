using DiplomaAPI.Models;
using DiplomaAPI.ViewModels.ReferralPackage;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IReferralPackageRepository
    {
        public List<ReferralPackage> getAll(int patientId);

        public List<ReferralPackage> GetByAmbulatoryEpisodeId(int episodeId);

        public List<ReferralPackage> GetByInpatientEpisodeId(int episodeId);

        public List<ReferralPackage> getMyReferrals(int doctorId);

        public List<ReferralPackage> getById(string id);

        public ReferralPackageViewModel Create(CreateReferralPackageViewModel data);
    }
}
