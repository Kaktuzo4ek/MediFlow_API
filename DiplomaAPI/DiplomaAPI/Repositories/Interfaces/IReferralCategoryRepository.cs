using DiplomaAPI.Models;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IReferralCategoryRepository
    {
        public List<ReferralCategory> getAll();

        public ReferralCategory getById(int id);
    }
}
