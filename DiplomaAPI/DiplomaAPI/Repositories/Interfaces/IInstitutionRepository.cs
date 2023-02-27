using DiplomaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IInstitutionRepository
    {
        public List<Institution> getAll();

        public Institution getById(int id);
    }
}
