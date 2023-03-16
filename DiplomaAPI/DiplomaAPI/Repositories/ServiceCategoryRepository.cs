using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;

namespace DiplomaAPI.Repositories
{
    public class ServiceCategoryRepository : IServiceCategoryRepository
    {
        private DataContext _data;
        public ServiceCategoryRepository(DataContext data)
        {
            _data = data;
        }

        public List<ServiceCategory> getAll()
        {
            var referralCategories = _data.ServiceCategories.ToList();
            return referralCategories;
        }

        public ServiceCategory getById(int id)
        {
            var referralCategory = _data.ServiceCategories.Find(id);
            return referralCategory;
        }
    }
}
