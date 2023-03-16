using DiplomaAPI.Models;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IServiceCategoryRepository
    {
        public List<ServiceCategory> getAll();

        public ServiceCategory getById(int id);
    }
}
