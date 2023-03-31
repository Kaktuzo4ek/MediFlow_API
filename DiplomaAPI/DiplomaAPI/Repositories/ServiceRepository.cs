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

        public List<Service> getProcedures()
        {
            var services = _data.Services.ToList();
            services.ForEach(service =>
            {
                _data.Entry(service).Reference("Category").Load();
            });

            services = services.Where(s => (s.Category.CategoryName.ToLower()).Contains("Процедура".ToLower())).ToList();

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
