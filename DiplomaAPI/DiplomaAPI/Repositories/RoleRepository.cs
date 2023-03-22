using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;

namespace DiplomaAPI.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private DataContext _data;
        public RoleRepository(DataContext data)
        {
            _data = data;
        }

        public List<Role> getAll()
        {
            var roles = _data.Roles.ToList();
            return roles;
        }

        public Role getById(int id)
        {
            var role = _data.Roles.Find(id);
            return role;
        }
    }
}
