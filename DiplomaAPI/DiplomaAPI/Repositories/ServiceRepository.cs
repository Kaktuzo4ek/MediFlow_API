using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;

namespace DiplomaAPI.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private DataContext _data;
        public ServiceRepository(DataContext data)
        {
            _data = data;
        }

        public List<Service> getAll()
        {
            var services = _data.Services.ToList();
            return services;
        }

        public Service getById(string id)
        {
            var service = _data.Services.Find(id);
            return service;
        }
    }
}
