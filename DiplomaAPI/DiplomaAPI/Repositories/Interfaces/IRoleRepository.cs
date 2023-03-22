using DiplomaAPI.Models;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        public List<Role> getAll();

        public Role getById(int id);
    }
}
