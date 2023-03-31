using DiplomaAPI.Models;
using DiplomaAPI.ViewModels.AmbulatoryEpisode.Appointment;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        public Appointment GetAppointment(int id);

        public List<Appointment> GetAllAppointments(int episodeId);

        public AppointmentViewModel CreateAppointment(CreateAppointmentViewModel model);

        public AppointmentViewModel Update(UpdateAppointmentViewModel model);
    }
}
