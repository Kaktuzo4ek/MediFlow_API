using DiplomaAPI.Models;
using DiplomaAPI.ViewModels.AmbulatoryEpisode;
using DiplomaAPI.ViewModels.Referral;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IAmbulatoryEpisodeRepository
    {
        public List<AmbulatoryEpisode> GetAll(int patientId);

        public AmbulatoryEpisodeViewModel Create(CreateAmbulatoryEpisodeViewModel model);

        public AmbulatoryEpisodeViewModel Update(UpdateAmbulatoryEpisodeViewModel model);

        public AmbulatoryEpisodeViewModel Delete(int id);
    }
}
