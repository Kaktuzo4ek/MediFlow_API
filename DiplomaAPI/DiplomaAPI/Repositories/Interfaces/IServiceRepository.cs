using DiplomaAPI.Models;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        public List<Service> getAll();

        public Service getById(string id);
    }
}
