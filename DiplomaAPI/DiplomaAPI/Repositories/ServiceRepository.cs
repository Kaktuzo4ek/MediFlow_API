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
            services.ForEach(service =>
            {
                _data.Entry(service).Reference("Category").Load();
            });
            return services;
        }

        public Service getById(string id)
        {
            var service = _data.Services.Find(id);
            _data.Entry(service).Reference("Category").Load();
            return service;
        }
    }
}
