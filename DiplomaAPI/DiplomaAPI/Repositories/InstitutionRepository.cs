using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiplomaAPI.Repositories
{
    public class InstitutionRepository : IInstitutionRepository
    {
        private DataContext _data;
        public InstitutionRepository(DataContext data)
        {
            _data = data;
        }

        public List<Institution> getAll()
        {
            var intsitutions = _data.Institutions.ToList();
            return intsitutions;
        }

        public Institution getById(int id)
        {
            var institution = _data.Institutions.Find(id);
            return institution;
        }
    }
}