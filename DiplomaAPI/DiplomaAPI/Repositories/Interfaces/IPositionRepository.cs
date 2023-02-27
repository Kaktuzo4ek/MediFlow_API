using DiplomaAPI.Models;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IPositionRepository
    {
        public List<Position> getAll();

        public Position getById(int id);
    }
}
