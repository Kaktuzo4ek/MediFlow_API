using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;

namespace DiplomaAPI.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private DataContext _data;
        public PositionRepository(DataContext data)
        {
            _data = data;
        }

        public List<Position> getAll()
        {
            var positions = _data.Positions.ToList();
            return positions;
        }

        public Position getById(int id)
        {
            var position = _data.Positions.Find(id);
            return position;
        }
    }
}
