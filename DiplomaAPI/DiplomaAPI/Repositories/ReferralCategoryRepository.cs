using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;

namespace DiplomaAPI.Repositories
{
    public class ReferralCategoryRepository : IReferralCategoryRepository
    {
        private DataContext _data;
        public ReferralCategoryRepository(DataContext data)
        {
            _data = data;
        }

        public List<ReferralCategory> getAll()
        {
            var referralCategories = _data.ReferralCategories.ToList();
            return referralCategories;
        }

        public ReferralCategory getById(int id)
        {
            var referralCategory = _data.ReferralCategories.Find(id);
            return referralCategory;
        }
    }
}
