using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.ReferralPackage;
using SendGrid.Helpers.Errors.Model;
using DiplomaAPI.ViewModels;
using DiplomaAPI.ViewModels.AmbulatoryEpisode;

namespace DiplomaAPI.Repositories
{
    public class AmbulatoryEpisodeRepository : IAmbulatoryEpisodeRepository
    {
        private DataContext _data;
        public AmbulatoryEpisodeRepository(DataContext data)
        {
            _data = data;
        }

        public List<AmbulatoryEpisode> GetAll(int patientId)
        {
            var episodes = _data.AmbulatoryEpisodes.Where(x => x.Patient.PatientId == patientId).ToList();

            episodes.ForEach(episode =>
            {
                _data.Entry(episode).Reference("Doctor").Load();
                _data.Entry(episode).Reference("Patient").Load();
                _data.Entry(episode).Reference("Appointment").Load();
                _data.Entry(episode).Reference("DiagnosisMKX10AM").Load();
                _data.Entry(episode).Reference("ReferralPackage").Load();
                _data.Entry(episode).Reference("Procedure").Load();
            });

            return episodes;
        }

        public AmbulatoryEpisodeViewModel Create(CreateAmbulatoryEpisodeViewModel model)
        {

            var diagnosis = _data.DiagnosesMKX10AM.Find(model.DiagnosisId);
            var test = 0;
            var episode = new AmbulatoryEpisode
            {
                Doctor = _data.Doctors.Find(model.DoctorId),
                Patient = _data.Patients.Find(model.PatientId),
                DiagnosisMKX10AM = _data.DiagnosesMKX10AM.Find(model.DiagnosisId),
                Status = "Діючий",
                Name = model.Name,
                Type = model.Type,
                DateCreated = DateTime.Now,
            };

            _data.AmbulatoryEpisodes.Add(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public AmbulatoryEpisodeViewModel Update(UpdateAmbulatoryEpisodeViewModel model)
        {
            var episode = _data.AmbulatoryEpisodes.Find(model.EpisodeId);

            if(episode == null)
                throw new NotFoundException();

            episode.DiagnosisMKX10AM = _data.DiagnosesMKX10AM.Find(model.DiagnosisId);

            if (model.Name != "")
                episode.Name = model.Name;

            if (model.Type != "")
                episode.Type = model.Type;


            _data.Update(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        public AmbulatoryEpisodeViewModel Delete(int id)
        {
            var episode = _data.AmbulatoryEpisodes.Find(id);

            if (episode == null)
                throw new NotFoundException();

            _data.AmbulatoryEpisodes.Remove(episode);
            _data.SaveChanges();

            return PrepareResponse(episode);
        }

        private AmbulatoryEpisodeViewModel PrepareResponse(AmbulatoryEpisode episode)
        {
            return new AmbulatoryEpisodeViewModel
            {
                EpisodeId = episode.EpisodeId,
                Doctor = episode.Doctor,
                Patient = episode.Patient,
                Status = episode.Status,
                Name = episode.Name,
                Type = episode.Type,
                DateCreated = episode.DateCreated
            };
        }
    }
}
