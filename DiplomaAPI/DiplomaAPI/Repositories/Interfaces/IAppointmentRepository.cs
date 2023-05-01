using DiplomaAPI.Models;
using DiplomaAPI.ViewModels;
using DiplomaAPI.ViewModels.AmbulatoryEpisode.Appointment;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        public Appointment GetAppointment(int id);

        public List<Appointment> GetAllAppointments(int episodeId);

        Task<UserManagerResponse> CreateAppointment(CreateAppointmentViewModel model);

        public AppointmentViewModel Update(int episodeId, UpdateAppointmentViewModel model);

        public AppointmentViewModel Delete(int episodeId, int appointmentId);
    }
}
